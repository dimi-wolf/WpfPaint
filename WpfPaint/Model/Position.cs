using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPaint.Model
{
    /// <summary>
    /// The position of an object.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableObject" />
    public partial class Position : ObservableObject
    {
        /// <summary>
        /// Gets or sets the x value.
        /// </summary>
        [ObservableProperty]
        private double _x;

        /// <summary>
        /// Gets or sets the y value.
        /// </summary>
        [ObservableProperty]
        private double _y;

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
