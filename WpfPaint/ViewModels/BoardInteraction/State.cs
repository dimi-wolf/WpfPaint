using System.Windows;

namespace WpfPaint.ViewModels.BoardInteraction
{
    /// <summary>
    /// Represents an abstract state.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="boardViewModel">The board view model.</param>
        protected State(BoardViewModel boardViewModel)
        {
            Board = boardViewModel;
        }

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void SetContext(Context context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected Context? Context { get; private set; }

        /// <summary>
        /// Gets the draft.
        /// </summary>
        /// <value>
        /// The draft.
        /// </value>
        protected BoardViewModel Board { get; }

        /// <summary>
        /// Handles the click event.
        /// </summary>
        /// <param name="clickPosition">The click position.</param>
        public virtual void HandleClick(Point clickPosition)
        {
        }

        /// <summary>
        /// Handles the move event.
        /// </summary>
        /// <param name="currentPosition">The current position.</param>
        public virtual void HandleMove(Point currentPosition)
        {
        }
    }
}
