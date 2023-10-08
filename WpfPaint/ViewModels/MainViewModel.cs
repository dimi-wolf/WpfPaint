namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The main view model of the application.
    /// </summary>
    public class MainViewModel
    {
        private readonly HeaderViewModel _headerViewModel;
        private readonly ObjectsViewModel _objectsViewModel;
        private readonly PropertiesViewModel _propertiesViewModel;
        private readonly FooterViewModel _footerViewModel;
        private readonly BoardViewModel _boardViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="headerViewModel">The header view model.</param>
        /// <param name="objectsViewModel">The objects view model.</param>
        /// <param name="boardViewModel">The board view model.</param>
        /// <param name="propertiesViewModel">The properties view model.</param>
        /// <param name="footerViewModel">The footer view model.</param>
        public MainViewModel(
            HeaderViewModel headerViewModel,
            ObjectsViewModel objectsViewModel,
            BoardViewModel boardViewModel,
            PropertiesViewModel propertiesViewModel,
            FooterViewModel footerViewModel)
        {
            _headerViewModel = headerViewModel;
            _objectsViewModel = objectsViewModel;
            _boardViewModel = boardViewModel;
            _propertiesViewModel = propertiesViewModel;
            _footerViewModel = footerViewModel;
        }

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public HeaderViewModel Header => _headerViewModel;

        /// <summary>
        /// Gets the objects.
        /// </summary>
        /// <value>
        /// The objects.
        /// </value>
        public ObjectsViewModel Objects => _objectsViewModel;

        /// <summary>
        /// Gets the board.
        /// </summary>
        /// <value>
        /// The board.
        /// </value>
        public BoardViewModel Board => _boardViewModel;

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public PropertiesViewModel Properties => _propertiesViewModel;

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public FooterViewModel Footer => _footerViewModel;
    }
}
