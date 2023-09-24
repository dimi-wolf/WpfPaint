using System.Collections.ObjectModel;
using WpfPaint.Model;
using WpfPaint.MVVM;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the drawing board.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ViewModelBase" />
    internal class BoardViewModel : ViewModelBase
    {
        private readonly ObjectsCollection _objectsCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardViewModel"/> class.
        /// </summary>
        /// <param name="objectsCollection">The objects collection.</param>
        public BoardViewModel(ObjectsCollection objectsCollection)
        {
            _objectsCollection = objectsCollection;
        }

        /// <summary>
        /// Gets all objects drawn on the board.
        /// </summary>
        public ObservableCollection<object> Objects => _objectsCollection.Objects;
    }
}
