namespace WpfPaint.Model
{
    /// <summary>
    /// The rectangle primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.Model.PrimitiveBase" />
    public class Rectangle : PrimitiveBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle()
        {
            Name = "Rectangle";
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
            get => GetValue<double>();
            set => SetValue(value);
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height
        {
            get => GetValue<double>();
            set => SetValue(value);
        }
    }
}
