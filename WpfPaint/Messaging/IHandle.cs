using System.Threading.Tasks;

namespace WpfPaint.Messaging
{
    /// <summary>
    /// Marker iterface for the <see cref="EventAggregator"/> to handle messages.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Is needed as marker interface for the event aggregator.")]
    public interface IHandle { }

    /// <summary>
    /// Defines a handler to handling the message of a specific type.
    /// </summary>
    /// <typeparam name="TMessage">The message type.</typeparam>
    public interface IHandle<TMessage> : IHandle
        where TMessage : class
    {
        /// <summary>
        /// Handles the given message.
        /// </summary>
        /// <param name="message">The message to be handeled.</param>
        /// <returns>
        /// An awaitable task.
        /// </returns>
        Task HandleMessageAsync(TMessage message);
    }
}
