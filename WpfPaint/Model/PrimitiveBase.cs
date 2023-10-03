using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MVVM.ComponentModel;
using MVVM.Messaging;
using WpfPaint.Messages;

namespace WpfPaint.Model
{
    /// <summary>
    /// The base class of an primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ValidationModelBase" />
    public abstract class PrimitiveBase : ValidationModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _name = string.Empty;
        private bool _showControls;
        private bool _isVisible;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveBase"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        protected PrimitiveBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _isVisible = true;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Der Name darf nicht leer sein.")]
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the controls should be shown.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the controls are shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowControls
        {
            get => _showControls;
            set => SetValue(ref _showControls, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsVisible
        {
            get => _isVisible;
            set => SetValue(ref _isVisible, value);
        }

        /// <summary>
        /// Sets this instance as the selected object.
        /// </summary>
        public async Task SetAsSelectedAsync()
        {
            await _eventAggregator.SendMessageAsync(new SetSelectedObjectMessage(this))
                .ConfigureAwait(true);
        }
    }
}
