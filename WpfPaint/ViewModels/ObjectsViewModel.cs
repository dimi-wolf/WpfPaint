using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfPaint.Messages;
using WpfPaint.Messaging;
using WpfPaint.Model;
using WpfPaint.MVVM;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// Manages the objects which are drawn on the board.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ViewModelBase" />
    internal class ObjectsViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ObjectsCollection _objectsCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectsViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="objectsCollection">The objects collection.</param>
        public ObjectsViewModel(
            IEventAggregator eventAggregator,
            ObjectsCollection objectsCollection)
        {
            _eventAggregator = eventAggregator;
            _objectsCollection = objectsCollection;

            RectangleCommand = new(param => AddObjectAsync(ObjectTypes.Rectangle));
            CircleCommand = new(param => AddObjectAsync(ObjectTypes.Circle));
            PolyLineCommand = new(param => AddObjectAsync(ObjectTypes.PolyLine));
            RemoveSelectedObjectCommand = new RelayCommand(param => RemoveSelectedObject(), param => CanRemoveSelectedObject);
        }

        /// <summary>
        /// Gets all objects drawn on the board.
        /// </summary>
        public ObservableCollection<object> Objects => _objectsCollection.Objects;

        /// <summary>
        /// Gets the selected object.
        /// </summary>
        public object? SelectedObject
        {
            get => GetValue<object>();
            set
            {
                OnSelectedObjectChanging(value);
                SetValue(value);
                _ = OnSelectedObjectChanged();
            }
        }

        /// <summary>
        /// Gets the rectangle command.
        /// </summary>
        /// <value>
        /// The rectangle command.
        /// </value>
        public RelayCommand RectangleCommand { get; }

        /// <summary>
        /// Gets the circle command.
        /// </summary>
        /// <value>
        /// The circle command.
        /// </value>
        public RelayCommand CircleCommand { get; }

        /// <summary>
        /// Gets the poly line command.
        /// </summary>
        /// <value>
        /// The poly line command.
        /// </value>
        public RelayCommand PolyLineCommand { get; }

        /// <summary>
        /// Gets the remove selected object command.
        /// </summary>
        /// <value>
        /// The remove selected object command.
        /// </value>
        public RelayCommand RemoveSelectedObjectCommand { get; }

        /// <summary>
        /// Gets a value indicating whether the selected object can be removed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this the selected object can be removed; otherwise, <c>false</c>.
        /// </value>
        public bool CanRemoveSelectedObject => SelectedObject != null;

        private void AddObjectAsync(ObjectTypes objectType)
        {
            _objectsCollection.AddNewObject(objectType);
            SelectedObject = Objects.Last();
        }

        private void OnSelectedObjectChanging(object? newValue)
        {
            if (SelectedObject is PrimitiveBase oldPrimitive)
            {
                oldPrimitive.ShowControls = false;
            }

            if (newValue is PrimitiveBase newPrimitive)
            {
                newPrimitive.ShowControls = true;
            }
        }

        private async Task OnSelectedObjectChanged()
        {
            await _eventAggregator.SendMessageAsync(new SelectedObjectChangedMessage(SelectedObject));
        }

        private void RemoveSelectedObject()
        {
            if (SelectedObject != null)
            {
                Objects.Remove(SelectedObject);
                SelectedObject = Objects.FirstOrDefault();
            }
        }
    }
}
