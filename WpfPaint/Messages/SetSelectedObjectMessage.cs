namespace WpfPaint.Messages
{
    /// <summary>
    /// A message which is sent when the selected object has to change.
    /// </summary>
    public class SetSelectedObjectMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetSelectedObjectMessage"/> class.
        /// </summary>
        /// <param name="selectedObject">The selected object.</param>
        public SetSelectedObjectMessage(object? selectedObject)
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
