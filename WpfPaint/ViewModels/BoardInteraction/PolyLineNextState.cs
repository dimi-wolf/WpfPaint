using System.Linq;
using System.Windows;
using WpfPaint.Model;

namespace WpfPaint.ViewModels.BoardInteraction
{
    public class PolyLineNextState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolyLineNextState"/> class.
        /// </summary>
        /// <param name="boardViewModel">The board view model.</param>
        public PolyLineNextState(BoardViewModel boardViewModel)
            : base(boardViewModel)
        {
        }

        /// <inheritdoc/>
        public override void HandleClick(Point clickPosition)
        {
            if (Board.DraftObject is PolyLine line)
            {
                line.AddPoint(new Position { X = clickPosition.X, Y = clickPosition.Y });
            }
        }

        public override void HandleMove(Point currentPosition)
        {
            if (Board.DraftObject is PolyLine line)
            {
                var lastPoint = line.Points.Last();
                lastPoint.X = currentPosition.X;
                lastPoint.Y = currentPosition.Y;
                line.UpdatePointsCollection();
            }
        }
    }
}
