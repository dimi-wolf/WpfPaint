using System.Threading.Tasks;
using WpfPaint.Messages;
using WpfPaint.Messaging;

namespace WpfPaint.Model
{
    /// <summary>
    /// The rectangle primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.Model.PrimitiveBase" />
    public class Rectangle : PrimitiveBase
    {
        private double _width;
        private double _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public Rectangle(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
            Name = "Rechteck";
            Width = 100;
            Height = 100;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Position Position { get; } = new Position();

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width
        {
            get => _width;
            set => SetValue(ref _width, CheckedValue(value));
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height
        {
            get => _height;
            set => SetValue(ref _height, CheckedValue(value));
        }

        private static double CheckedValue(double value) => value < 0 ? 0 : value;
    }
}
