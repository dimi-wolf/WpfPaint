using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace WpfPaint.Model
{
    /// <summary>
    /// The circle primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.Model.PrimitiveBase" />
    public partial class Circle : PrimitiveBase
    {
        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        [ObservableProperty]
        private double _radius = 100d;

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [ObservableProperty]
        private Position _position = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        public Circle()
            : base(WeakReferenceMessenger.Default)
        {
            Name = Resources.Strings.Circle;
        }
    }
}
