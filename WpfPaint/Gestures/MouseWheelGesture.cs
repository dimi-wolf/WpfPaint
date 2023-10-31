using System.Windows.Input;

namespace WpfPaint.Gestures
{
    /// <summary>
    /// A mouse gesture to bind Commands.
    /// </summary>
    /// <seealso cref="System.Windows.Input.MouseGesture" />
    public class MouseWheelGesture : MouseGesture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelGesture"/> class.
        /// </summary>
        public MouseWheelGesture() : base(MouseAction.WheelClick) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelGesture"/> class.
        /// </summary>
        /// <param name="modifiers">The modifiers.</param>
        public MouseWheelGesture(ModifierKeys modifiers) : base(MouseAction.WheelClick, modifiers) { }

        /// <summary>
        /// Gets the control down.
        /// </summary>
        /// <value>
        /// The control down.
        /// </value>
        public static MouseWheelGesture CtrlDown => new(ModifierKeys.Control) { Direction = MouseWheelDirection.Down };

        /// <summary>
        /// Gets the control up.
        /// </summary>
        /// <value>
        /// The control up.
        /// </value>
        public static MouseWheelGesture CtrlUp => new(ModifierKeys.Control) { Direction = MouseWheelDirection.Up };

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public MouseWheelDirection Direction { get; set; }

        /// <summary>
        /// Matcheses the specified target element.
        /// </summary>
        /// <param name="targetElement">The target element.</param>
        /// <param name="inputEventArgs">The <see cref="InputEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            bool result = base.Matches(targetElement, inputEventArgs)
                && inputEventArgs is MouseWheelEventArgs args
                && Direction switch
                {
                    MouseWheelDirection.None => args.Delta == 0,
                    MouseWheelDirection.Up => args.Delta > 0,
                    MouseWheelDirection.Down => args.Delta < 0,
                    _ => false,
                };

            return result;
        }
    }
}
