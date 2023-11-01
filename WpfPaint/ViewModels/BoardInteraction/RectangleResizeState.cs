using System.Windows;
using WpfPaint.Model;

namespace WpfPaint.ViewModels.BoardInteraction
{
    public class RectangleResizeState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleResizeState"/> class.
        /// </summary>
        /// <param name="boardViewModel">The board view model.</param>
        public RectangleResizeState(BoardViewModel boardViewModel)
            : base(boardViewModel)
        {
        }

        /// <inheritdoc/>
        public override void HandleClick(Point clickPosition)
        {
            if (Board.DraftObject is Rectangle rect)
            {
                rect.Height = clickPosition.Y - rect.Position.Y;
                rect.Width = clickPosition.X - rect.Position.X;
                Board.CompleteDraft();
            }
        }

        /// <inheritdoc/>
        public override void HandleMove(Point currentPosition)
        {
            if (Board.DraftObject is Rectangle rect)
            {
                rect.Height = currentPosition.Y - rect.Position.Y;
                rect.Width = currentPosition.X - rect.Position.X;
            }
        }
    }
}
