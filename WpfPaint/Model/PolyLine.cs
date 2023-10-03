using System.Collections.ObjectModel;
using System.Linq;
using MVVM.Input;
using MVVM.Messaging;

namespace WpfPaint.Model
{
    /// <summary>
    /// The poly line primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.Model.PrimitiveBase" />
    public class PolyLine : PrimitiveBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolyLine"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public PolyLine(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
            Name = "Pfad";
            Points.Add(new Position { X = 10, Y = 10 });
            Points.Add(new Position { X = 90, Y = 90 });
            AddPointCommand = new RelayCommand(AddPoint);
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Position Position { get; } = new Position();

        /// <summary>
        /// Gets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public ObservableCollection<Position> Points { get; } = new();

        /// <summary>
        /// Gets the add point command.
        /// </summary>
        /// <value>
        /// The add point command.
        /// </value>
        public RelayCommand AddPointCommand { get; }

        /// <summary>
        /// Updates the points collection.
        /// </summary>
        public void UpdatePointsCollection()
        {
            NotifyPropertyChanged(nameof(Points));
        }

        /// <summary>
        /// Adds a point.
        /// </summary>
        private void AddPoint()
        {
            Position last = Points.Last();
            Points.Add(new Position { X = last.X + 50, Y = last.Y + 50 });
            UpdatePointsCollection();
        }
    }
}
