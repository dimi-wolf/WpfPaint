using System.Linq;
using System.Windows;
using System.Windows.Shapes;
using WpfPaint.Model;

namespace WpfPaint.ViewModels.BoardInteraction
{
    public class PolyLineBeginState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolyLineBeginState"/> class.
        /// </summary>
        /// <param name="boardViewModel">The board view model.</param>
        public PolyLineBeginState(BoardViewModel boardViewModel)
            : base(boardViewModel)
        {
        }

        /// <inheritdoc/>
        public override void HandleClick(Point clickPosition)
        {
            if (Board.DraftObject is PolyLine line)
            {
                var firstPoint = line.Points.First();
                firstPoint.X = clickPosition.X;
                firstPoint.Y = clickPosition.Y;
                line.UpdatePointsCollection();
                line.IsVisible = true;
                Context?.TransitionTo(new PolyLineNextState(Board));
            }
        }
    }
}
