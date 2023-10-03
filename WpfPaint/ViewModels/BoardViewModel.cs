using System.Collections.ObjectModel;
using MVVM.ComponentModel;
using WpfPaint.Model;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the drawing board.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ViewModelBase" />
    public class BoardViewModel : ViewModelBase
    {
        private readonly ObjectsStore _objectsCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardViewModel"/> class.
        /// </summary>
        /// <param name="objectsCollection">The objects collection.</param>
        public BoardViewModel(ObjectsStore objectsCollection)
        {
            _objectsCollection = objectsCollection;
        }

        /// <summary>
        /// Gets all objects drawn on the board.
        /// </summary>
        public ObservableCollection<object> Objects => _objectsCollection.Objects;
    }
}
