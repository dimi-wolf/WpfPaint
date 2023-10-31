using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfPaint.Model;
using WpfPaint.ViewModels;

namespace WpfPaint.Views
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        private Point _lastDragPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardView"/> class.
        /// </summary>
        public BoardView()
        {
            InitializeComponent();
        }

        private BoardViewModel ViewModel => (BoardViewModel)DataContext;

        private bool CanMove(MouseEventArgs mouseEventArgs) =>
            mouseEventArgs.LeftButton == MouseButtonState.Pressed && ViewModel.IsMove ||
            mouseEventArgs.RightButton == MouseButtonState.Pressed;

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (CanMove(e))
            {
                Point currentPosition = e.GetPosition(_scrollViewer);
                Vector mouseChange = currentPosition - _lastDragPosition;
                _lastDragPosition = currentPosition;

                _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset - mouseChange.X);
                _scrollViewer.ScrollToVerticalOffset(_scrollViewer.VerticalOffset - mouseChange.Y);
            }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (DataContext is BoardViewModel viewModel)
            {
                if (viewModel.IsMove)
                {
                    Cursor = Cursors.Hand;
                }
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is BoardViewModel viewModel)
            {
                if (viewModel.IsMove)
                {
                    Cursor = Cursors.Arrow;
                }
            }
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CanMove(e))
            {
                _lastDragPosition = e.GetPosition(_scrollViewer);
                _scrollViewer.CaptureMouse();
            }
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _scrollViewer.ReleaseMouseCapture();
        }

        private void OnObjectsControlPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = Mouse.GetPosition(_objectsControl);
            ViewModel.HandleClick(position);
        }

        private void OnObjectsControlMouseMove(object sender, MouseEventArgs e)
        {
            Point position = Mouse.GetPosition(_objectsControl);
            ViewModel.HandleMove(position);
        }

        private void OnObjectsControlPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.CompleteDraft();
        }
    }
}
