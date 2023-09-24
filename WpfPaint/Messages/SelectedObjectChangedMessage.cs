namespace WpfPaint.Messages
{
    /// <summary>
    /// A message which is send when the selected object has changed.
    /// </summary>
    public class SelectedObjectChangedMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedObjectChangedMessage"/> class.
        /// </summary>
        /// <param name="selectedObject">The selected object.</param>
        public SelectedObjectChangedMessage(object? selectedObject)
        {
            SelectedObject = selectedObject;
        }

        /// <summary>
        /// Gets the selected object.
        /// </summary>
        /// <value>
        /// The selected object.
        /// </value>
        public object? SelectedObject { get; }
    }
}
