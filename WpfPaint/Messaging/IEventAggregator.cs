using System.Threading.Tasks;

namespace WpfPaint.Messaging
{
    /// <summary>
    /// The event aggregator to communicate between objects.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Subscribes the given handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>
        /// An awaitable task.
        /// </returns>
        Task SubscribeAsync(IHandle handler);

        /// <summary>
        /// Unsubscribes the given handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>
        /// An awaitable task.
        /// </returns>
        Task UnsubscribeAsync(IHandle handler);

        /// <summary>
        /// Sends the given message to all subscribers.
        /// </summary>
        /// <typeparam name="TMessage">The message type.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>
        /// An awaitable task.
        /// </returns>
        Task SendMessageAsync<TMessage>(TMessage message) where TMessage : class;
    }
}
