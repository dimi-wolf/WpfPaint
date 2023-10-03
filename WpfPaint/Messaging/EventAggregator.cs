using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WpfPaint.Messaging
{
    /// <summary>
    /// The event aggregator to communicate between objects.
    /// </summary>
    public class EventAggregator : IEventAggregator, IDisposable
    {
        private readonly List<IHandle> _handlers = new();
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private bool disposedValue;

        /// <summary>
        /// Subscribes the given handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public async Task SubscribeAsync(IHandle handler)
        {
            await _semaphore.WaitAsync()
                .ConfigureAwait(false);

            try
            {
                _handlers.Add(handler);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Unsubscribes the given handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public async Task UnsubscribeAsync(IHandle handler)
        {
            await _semaphore.WaitAsync()
                .ConfigureAwait(false);

            try
            {
                _handlers.Remove(handler);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Sends the given message to all subscribers.
        /// </summary>
        /// <typeparam name="TMessage">The message type.</typeparam>
        /// <param name="message">The message.</param>
        public async Task SendMessageAsync<TMessage>(TMessage message) where TMessage : class
        {
            List<IHandle<TMessage>> relevantHandlers;

            await _semaphore.WaitAsync()
                .ConfigureAwait(true);

            try
            {
                relevantHandlers = _handlers
                    .OfType<IHandle<TMessage>>()
                    .ToList();
            }
            finally
            {
                _semaphore.Release();
            }

            foreach (IHandle<TMessage> handler in relevantHandlers)
            {
                await handler.HandleMessageAsync(message)
                    .ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _semaphore.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
