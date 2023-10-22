using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WpfPaint.Authorization;
using WpfPaint.Messages;
using WpfPaint.Model;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// Manages the objects which are drawn on the board.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableRecipient" />
    /// <seealso cref="CommunityToolkit.Mvvm.Messaging.IRecipient&lt;WpfPaint.Messages.SetSelectedObjectMessage&gt;" />
    public partial class ObjectsViewModel : ObservableRecipient, IRecipient<SetSelectedObjectMessage>
    {
        private readonly ObjectsStore _objectsCollection;
        private readonly IAuthorizationService _authorizationService;
        private object? _selectedObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectsViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="objectsCollection">The objects collection.</param>
        public ObjectsViewModel(ObjectsStore objectsCollection, IAuthorizationService authorizationService)
        {
            _objectsCollection = objectsCollection;
            _authorizationService = authorizationService;
            IsActive = true;
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
            set => SetProperty(_selectedObject, value, callback: OnSelectedObjectChanging);
        }

        /// <summary>
        /// Gets a value indicating whether the selected object can be removed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this the selected object can be removed; otherwise, <c>false</c>.
        /// </value>
        public bool CanRemoveSelectedObject => _authorizationService.HasPermission(Roles.Designers) && SelectedObject != null;

        /// <summary>
        /// Gets a value indicating whether this instance can add object.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can add object; otherwise, <c>false</c>.
        /// </value>
        public bool CanAddObject => _authorizationService.HasPermission(Roles.Designers);

        /// <summary>
        /// Adds the object of the given type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        [RelayCommand(CanExecute = nameof(CanAddObject))]
        public void AddObject(ObjectTypes objectType)
        {
            _objectsCollection.AddNewObject(objectType);
            SelectedObject = Objects.Last();
        }

        /// <summary>
        /// Removes the selected object.
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanRemoveSelectedObject))]
        private void RemoveSelectedObject()
        {
            if (SelectedObject != null)
            {
                Objects.Remove(SelectedObject);
                SelectedObject = Objects.FirstOrDefault();
            }
        }

        /// <summary>
        /// Receives a given <typeparamref name="TMessage" /> message instance.
        /// </summary>
        /// <param name="message">The message being received.</param>
        public void Receive(SetSelectedObjectMessage message)
        {
            if (message != null)
            {
                SelectedObject = message.SelectedObject;
            }
        }

        private void OnSelectedObjectChanging(object? newValue)
        {
            if (_selectedObject is PrimitiveBase oldPrimitive)
            {
                oldPrimitive.ShowControls = false;
            }

            if (newValue is PrimitiveBase newPrimitive)
            {
                newPrimitive.ShowControls = _authorizationService.HasPermission(Roles.Designers);
            }

            _selectedObject = newValue;
            Messenger.Send(new SelectedObjectChangedMessage(SelectedObject));
        }
    }
}
