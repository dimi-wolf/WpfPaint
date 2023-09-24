using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WpfPaint.Model
{
    /// <summary>
    /// The circle primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.Model.PrimitiveBase" />
    public class Circle : PrimitiveBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        public Circle()
        {
            Name = "Kreis";
            Radius = 100;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Position Position { get; } = new Position();

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        public double Radius
        {
            get => GetValue<double>();
            set => SetValue(value);
        }
    }
}
