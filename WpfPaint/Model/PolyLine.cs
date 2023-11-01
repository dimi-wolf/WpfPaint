using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace WpfPaint.Model
{
    /// <summary>
    /// The poly line primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.Model.PrimitiveBase" />
    public partial class PolyLine : PrimitiveBase
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [ObservableProperty]
        private Position _position = new();

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<Position> _points = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyLine"/> class.
        /// </summary>
        public PolyLine()
            : base(WeakReferenceMessenger.Default)
        {
            Name = Resources.Strings.PolyLine;
            Points.Add(new Position());
            Points.Add(new Position());
        }

        /// <summary>
        /// Updates the points collection.
        /// </summary>
        public void UpdatePointsCollection()
        {
            OnPropertyChanged(nameof(Points));
        }

        /// <summary>
        /// Adds a point.
        /// </summary>
        [RelayCommand]
        public void AddPoint(Position? position = null)
        {
            Position last = Points.Last();
            position ??= new Position { X = last.X + 50, Y = last.Y + 50 };
            Points.Add(position);
            UpdatePointsCollection();
        }
    }
}
