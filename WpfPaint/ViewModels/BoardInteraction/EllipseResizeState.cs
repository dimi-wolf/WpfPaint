using System.Windows;
using WpfPaint.Model;

namespace WpfPaint.ViewModels.BoardInteraction
{
    public class EllipseResizeState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseResizeState"/> class.
        /// </summary>
        /// <param name="boardViewModel">The board view model.</param>
        public EllipseResizeState(BoardViewModel boardViewModel)
            : base(boardViewModel)
        {
        }

        /// <inheritdoc/>
        public override void HandleClick(Point clickPosition)
        {
            if (Board.DraftObject is Circle circ)
            {
                circ.Radius = clickPosition.X - circ.Position.X;
                Board.CompleteDraft();
            }
        }

        /// <inheritdoc/>
        public override void HandleMove(Point currentPosition)
        {
            if (Board.DraftObject is Circle circ)
            {
                circ.Radius = currentPosition.X - circ.Position.X;
            }
        }
    }
}
