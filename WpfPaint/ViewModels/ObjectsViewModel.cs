using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MVVM.ComponentModel;
using MVVM.Input;
using MVVM.Messaging;
using WpfPaint.Messages;
using WpfPaint.Model;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// Manages the objects which are drawn on the board.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ViewModelBase" />
    public class ObjectsViewModel : ViewModelBase, IHandle<SetSelectedObjectMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ObjectsStore _objectsCollection;
        private object? _selectedObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectsViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="objectsCollection">The objects collection.</param>
        public ObjectsViewModel(
            IEventAggregator eventAggregator,
            ObjectsStore objectsCollection)
        {
            _eventAggregator = eventAggregator;
            _objectsCollection = objectsCollection;

            RectangleCommand = new(() => AddObject(ObjectTypes.Rectangle));
            CircleCommand = new(() => AddObject(ObjectTypes.Circle));
            PolyLineCommand = new(() => AddObject(ObjectTypes.PolyLine));
            RemoveSelectedObjectCommand = new(RemoveSelectedObject, () => CanRemoveSelectedObject);
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
            get => _selectedObject;
            set
            {
                OnSelectedObjectChanging(value);
                SetValue(ref _selectedObject, value);
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

        private void AddObject(ObjectTypes objectType)
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
            await _eventAggregator.SendMessageAsync(new SelectedObjectChangedMessage(SelectedObject))
                .ConfigureAwait(true);
        }

        private void RemoveSelectedObject()
        {
            if (SelectedObject != null)
            {
                Objects.Remove(SelectedObject);
                SelectedObject = Objects.FirstOrDefault();
            }
        }

        public Task HandleMessageAsync(SetSelectedObjectMessage message)
        {
            if (message != null)
            {
                SelectedObject = message.SelectedObject;
            }

            return Task.CompletedTask;
        }

        public override async Task OnLoadingAsync()
        {
            await _eventAggregator.SubscribeAsync(this).ConfigureAwait(true);
            await base.OnLoadingAsync().ConfigureAwait(true);
        }

        public override async Task OnUnloadingAsync()
        {
            await _eventAggregator.UnsubscribeAsync(this).ConfigureAwait(true);
            await base.OnUnloadingAsync().ConfigureAwait(true);
        }
    }
}
