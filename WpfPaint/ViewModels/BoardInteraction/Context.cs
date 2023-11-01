using System.Windows;

namespace WpfPaint.ViewModels.BoardInteraction
{
    /// <summary>
    /// The context holds the current state and delegates the actions to the state.
    /// </summary>
    public class Context
    {
        private State? _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public Context(State state)
        {
            TransitionTo(state);
        }

        /// <summary>
        /// Changes the current state to the given state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void TransitionTo(State state)
        {
            _state = state;
            _state.SetContext(this);
        }

        /// <summary>
        /// Handles the click event.
        /// </summary>
        /// <param name="clickPosition">The click position.</param>
        public void HandleClick(Point clickPosition)
        {
            _state?.HandleClick(clickPosition);
        }

        /// <summary>
        /// Handles the move event.
        /// </summary>
        /// <param name="currentPosition">The current position.</param>
        public void HandleMove(Point currentPosition)
        {
            _state?.HandleMove(currentPosition);
        }
    }
}
