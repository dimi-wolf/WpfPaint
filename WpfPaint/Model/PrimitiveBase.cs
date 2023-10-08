using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using WpfPaint.Messages;

namespace WpfPaint.Model
{
    /// <summary>
    /// The base class of an primitive.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableValidator" />
    public abstract partial class PrimitiveBase : ObservableValidator
    {
        private readonly IMessenger _messenger;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ObservableProperty]
        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.ErrorNameRequired))]
        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets a value indication whether the controls should be shown.
        /// </summary>
        [ObservableProperty]
        private bool _showControls;

        /// <summary>
        /// Gets or sets a value indication whether the object is visible.
        /// </summary>
        [ObservableProperty]
        private bool _isVisible = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveBase"/> class.
        /// </summary>
        /// <param name="messenger">The messenger.</param>
        protected PrimitiveBase(IMessenger messenger)
        {
            _messenger = messenger;
        }

        /// <summary>
        /// Sets this primitve as selected.
        /// </summary>
        public void SetSelected()
        {
            _messenger.Send(new SetSelectedObjectMessage(this));
        }
    }
}
