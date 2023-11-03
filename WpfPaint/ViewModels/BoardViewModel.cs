using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WpfPaint.Authorization;
using WpfPaint.Messages;
using WpfPaint.Model;
using WpfPaint.ViewModels.BoardInteraction;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the drawing board.
    /// </summary>
    public partial class BoardViewModel : ObservableRecipient
    {
        private const double ZoomStep = 1.25;

        private readonly ObjectsStore _objectsCollection;
        private readonly IAuthorizationService _authorizationService;

        private InteractionMode _currentInteractionMode = InteractionMode.Move;
        private PrimitiveBase? _draftObject;
        private double _zoom = 1d;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardViewModel"/> class.
        /// </summary>
        /// <param name="objectsCollection">The objects collection.</param>
        /// <param name="authorizationService">The authorization service.</param>
        public BoardViewModel(ObjectsStore objectsCollection, IAuthorizationService authorizationService)
        {
            _objectsCollection = objectsCollection;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Gets all objects drawn on the board.
        /// </summary>
        public ObservableCollection<object> Objects => _objectsCollection.Objects;

        /// <summary>
        /// Gets or sets the current interaction mode.
        /// </summary>
        /// <value>
        /// The current interaction mode.
        /// </value>
        public InteractionMode CurrentInteractionMode
        {
            get => _currentInteractionMode;
            set
            {
                if (SetProperty(ref _currentInteractionMode, value))
                {
                    AbortDraft();
                    OnPropertyChanged(nameof(IsMove));
                    OnPropertyChanged(nameof(IsSelect));
                    OnPropertyChanged(nameof(IsEdit));
                    OnPropertyChanged(nameof(IsRectangle));
                    OnPropertyChanged(nameof(IsCircle));
                    OnPropertyChanged(nameof(IsPolyline));
                }
            }
        }

        /// <summary>
        /// Gets or sets the current object.
        /// </summary>
        /// <value>
        /// The current object.
        /// </value>
        public PrimitiveBase? DraftObject
        {
            get => _draftObject;
            private set => SetProperty(ref _draftObject, value);
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public Context? DraftContext { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is move.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is move; otherwise, <c>false</c>.
        /// </value>
        public bool IsMove
        {
            get => _currentInteractionMode == InteractionMode.Move;
            set
            {
                CurrentInteractionMode = InteractionMode.Move;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is select.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is select; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelect
        {
            get => _currentInteractionMode == InteractionMode.Select;
            set
            {
                CurrentInteractionMode = InteractionMode.Select;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is edit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is edit; otherwise, <c>false</c>.
        /// </value>
        public bool IsEdit
        {
            get => _currentInteractionMode == InteractionMode.Edit;
            set
            {
                CurrentInteractionMode = InteractionMode.Edit;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rectangle.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rectangle; otherwise, <c>false</c>.
        /// </value>
        public bool IsRectangle
        {
            get => _currentInteractionMode == InteractionMode.Rectangle;
            set => CurrentInteractionMode = InteractionMode.Rectangle;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is circle.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is circle; otherwise, <c>false</c>.
        /// </value>
        public bool IsCircle
        {
            get => _currentInteractionMode == InteractionMode.Circle;
            set => CurrentInteractionMode = InteractionMode.Circle;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is polyline.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is polyline; otherwise, <c>false</c>.
        /// </value>
        public bool IsPolyline
        {
            get => _currentInteractionMode == InteractionMode.Polyline;
            set => CurrentInteractionMode = InteractionMode.Polyline;
        }

        /// <summary>
        /// Gets the zoom values.
        /// </summary>
        /// <value>
        /// The zoom values.
        /// </value>
        public IEnumerable<double> ZoomValues { get; } = new List<double> { 0.25, 0.5, 0.75, 1.0, 1.5, 2.0, 4.0, 8.0 };

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        /// <value>
        /// The zoom.
        /// </value>
        public double Zoom
        {
            get => _zoom;
            set
            {
                if (value < 0.25)
                {
                    value = 0.25;
                }
                else if (value > 40)
                {
                    value = 40;
                }

                SetProperty(ref _zoom, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can add object.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can add object; otherwise, <c>false</c>.
        /// </value>
        public bool CanAddObject => _authorizationService.HasPermission(Roles.Designers);

        [RelayCommand]
        private void ZoomIn()
        {
            Zoom *= ZoomStep;
        }

        [RelayCommand]
        private void ZoomOut()
        {
            Zoom /= ZoomStep;
        }

        [RelayCommand(CanExecute = nameof(CanAddObject))]
        private void DrawRectangle()
        {
            DraftObject = _objectsCollection.AddNewObject(ObjectTypes.Rectangle) as Rectangle;

            if (DraftObject != null)
            {
                DraftObject.IsVisible = false;
                DraftObject.ShowControls = false;
                DraftContext = new Context(new RectangleBeginState(this));
            }
        }

        [RelayCommand(CanExecute = nameof(CanAddObject))]
        private void DrawCircle()
        {
            DraftObject = _objectsCollection.AddNewObject(ObjectTypes.Circle) as Circle;

            if (DraftObject != null)
            {
                DraftObject.IsVisible = false;
                DraftObject.ShowControls = false;
                DraftContext = new Context(new EllipseBeginState(this));
            }
        }

        [RelayCommand(CanExecute = nameof(CanAddObject))]
        private void DrawPolyLine()
        {
            DraftObject = _objectsCollection.AddNewObject(ObjectTypes.PolyLine) as PolyLine;

            if (DraftObject != null)
            {
                DraftObject.IsVisible = false;
                DraftObject.ShowControls = false;
                DraftContext = new Context(new PolyLineBeginState(this));
            }
        }

        public void HandleClick(Point clickPosition)
        {
            DraftContext?.HandleClick(clickPosition);
        }

        public void HandleMove(Point currentPosition)
        {
            DraftContext?.HandleMove(currentPosition);
        }

        public void CompleteDraft()
        {
            if (DraftObject != null)
            {
                Messenger.Send(new SetSelectedObjectMessage(DraftObject));
                DraftObject = null;
                DraftContext = null;
                IsEdit = true;
            }
        }

        [RelayCommand]
        public void AbortDraft()
        {
            if (DraftObject != null)
            {
                _objectsCollection.Objects.Remove(DraftObject);
                DraftObject = null;
            }

            DraftContext = null;
        }

        [RelayCommand]
        private void DeselectAll()
        {
            Messenger.Send(new SetSelectedObjectMessage(null));
        }
    }
}
