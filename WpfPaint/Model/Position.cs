using System.Windows;
using MVVM.ComponentModel;

namespace WpfPaint.Model
{
    /// <summary>
    /// The position of an object.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.PropertyChangedBase" />
    public class Position : PropertyChangedBase
    {
        private double _x;
        private double _y;

        /// <summary>
        /// Gets or sets the x value.
        /// </summary>
        /// <value>
        /// The x value.
        /// </value>
        public double X
        {
            get => _x;
            set => SetValue(ref _x, value);
        }

        /// <summary>
        /// Gets or sets the y value.
        /// </summary>
        /// <value>
        /// The y value.
        /// </value>
        public double Y
        {
            get => _y;
            set => SetValue(ref _y, value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Position"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Point(Position position) => position == null ? new() : new(position.X, position.Y);

        /// <summary>
        /// Converts to point.
        /// </summary>
        /// <returns></returns>
        public Point ToPoint() => new(X, Y);
    }
}
