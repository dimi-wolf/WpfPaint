using System.Threading.Tasks;
using MVVM.ComponentModel;
using MVVM.Messaging;
using WpfPaint.Messages;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the properties page.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ViewModelBase" />
    /// <seealso cref="WpfPaint.Messaging.IHandle&lt;WpfPaint.Messages.SelectedObjectChangedMessage&gt;" />
    public class PropertiesViewModel : ViewModelBase, IHandle<SelectedObjectChangedMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private object? _currentObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public PropertiesViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Gets or sets the current object.
        /// </summary>
        /// <value>
        /// The current object.
        /// </value>
        public object? CurrentObject
        {
            get => _currentObject;
            set => SetValue(ref _currentObject, value);
        }

        /// <summary>
        /// Handles the given message.
        /// </summary>
        /// <param name="message">The message to be handeled.</param>
        /// <returns>
        /// An awaitable task.
        /// </returns>
        public Task HandleMessageAsync(SelectedObjectChangedMessage message)
        {
            CurrentObject = message?.SelectedObject;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when the view model is loading.
        /// </summary>
        public override async Task OnLoadingAsync()
        {
            await _eventAggregator.SubscribeAsync(this).ConfigureAwait(true);
            await base.OnLoadingAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// Called when the view model is unloading.
        /// </summary>
        public override async Task OnUnloadingAsync()
        {
            await _eventAggregator.UnsubscribeAsync(this).ConfigureAwait(true);
            await base.OnUnloadingAsync().ConfigureAwait(true);
        }
    }
}
