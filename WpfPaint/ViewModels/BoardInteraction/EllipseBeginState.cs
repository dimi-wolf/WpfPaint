using System.Windows;
using WpfPaint.Model;

namespace WpfPaint.ViewModels.BoardInteraction
{
    public class EllipseBeginState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseBeginState"/> class.
        /// </summary>
        /// <param name="boardViewModel">The board view model.</param>
        public EllipseBeginState(BoardViewModel boardViewModel)
            : base(boardViewModel)
        {
        }

        /// <inheritdoc/>
        public override void HandleClick(Point clickPosition)
        {
            if (Board.DraftObject is Circle circ)
            {
                circ.Position.X = clickPosition.X;
                circ.Position.Y = clickPosition.Y;
                circ.IsVisible = true;
                Context?.TransitionTo(new EllipseResizeState(Board));
            }
        }
    }
}
