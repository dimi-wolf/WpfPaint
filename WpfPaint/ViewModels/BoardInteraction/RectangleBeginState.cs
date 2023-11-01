using System.Windows;
using WpfPaint.Model;

namespace WpfPaint.ViewModels.BoardInteraction
{
    public class RectangleBeginState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleBeginState"/> class.
        /// </summary>
        /// <param name="boardViewModel">The board view model.</param>
        public RectangleBeginState(BoardViewModel boardViewModel)
            : base(boardViewModel)
        {
        }

        /// <inheritdoc/>
        public override void HandleClick(Point clickPosition)
        {
            if (Board.DraftObject is Rectangle rect)
            {
                rect.Position.X = clickPosition.X;
                rect.Position.Y = clickPosition.Y;
                rect.IsVisible = true;
                Context?.TransitionTo(new RectangleResizeState(Board));
            }
        }
    }
}
