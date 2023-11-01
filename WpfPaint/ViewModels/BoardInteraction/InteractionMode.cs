namespace WpfPaint.ViewModels.BoardInteraction
{
    /// <summary>
    /// Defines the interaction modes for the drawing board.
    /// </summary>
    public enum InteractionMode
    {
        /// <summary>
        /// Move the board.
        /// </summary>
        Move,

        /// <summary>
        /// Select an object.
        /// </summary>
        Select,

        /// <summary>
        /// Edit the properties of an object.
        /// </summary>
        Edit,

        /// <summary>
        /// Draw a rectangle.
        /// </summary>
        Rectangle,

        /// <summary>
        /// Draw a circle.
        /// </summary>
        Circle,

        /// <summary>
        /// Draw a polyline.
        /// </summary>
        Polyline
    }
}
