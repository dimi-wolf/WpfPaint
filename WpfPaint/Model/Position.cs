using System.Windows;
using WpfPaint.MVVM;

namespace WpfPaint.Model
{
    /// <summary>
    /// The position of an object.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.PropertyChangedBase" />
    public class Position : PropertyChangedBase
    {
        /// <summary>
        /// Gets or sets the x value.
        /// </summary>
        /// <value>
        /// The x value.
        /// </value>
        public double X
        {
            get => GetValue<double>();
            set => SetValue(value);
        }

        /// <summary>
        /// Gets or sets the y value.
        /// </summary>
        /// <value>
        /// The y value.
        /// </value>
        public double Y
        {
            get => GetValue<double>();
            set => SetValue(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Position"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Point(Position position) => new(position.X, position.Y);
    }
}
