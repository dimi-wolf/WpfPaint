using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfPaint.Model;

namespace WpfPaint.Views.Primitives
{
    /// <summary>
    /// Interaction logic for RectangleView.xaml
    /// </summary>
    public partial class RectangleView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleView"/> class.
        /// </summary>
        public RectangleView()
        {
            InitializeComponent();
        }

        private void OnPositionDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.Position.X += e.HorizontalChange;
                rect.Position.Y += e.VerticalChange;
            }
        }

        private void OnLeftTopDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.Position.X += e.HorizontalChange;
                rect.Width -= e.HorizontalChange;
                rect.Position.Y += e.VerticalChange;
                rect.Height -= e.VerticalChange;
            }
        }

        private void OnRightTopDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.Width += e.HorizontalChange;
                rect.Position.Y += e.VerticalChange;
                rect.Height -= e.VerticalChange;
            }
        }

        private void OnLeftBottomDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.Position.X += e.HorizontalChange;
                rect.Width -= e.HorizontalChange;
                rect.Height += e.VerticalChange;
            }
        }

        private void OnRightBottomDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.Width += e.HorizontalChange;
                rect.Height += e.VerticalChange;
            }
        }

        private void OnRectangleMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.SetSelected();
            }
        }

        private void OnTopDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.Position.Y += e.VerticalChange;
                rect.Height -= e.VerticalChange;
            }
        }

        private void OnRightDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.Width += e.HorizontalChange;
            }
        }

        private void OnBottomDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.Height += e.VerticalChange;
            }
        }

        private void OnLeftDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Rectangle rect)
            {
                rect.Position.X += e.HorizontalChange;
                rect.Width -= e.HorizontalChange;
            }
        }
    }
}
