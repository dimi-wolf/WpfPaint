using System.Windows.Controls;
using WpfPaint.Model;

namespace WpfPaint.Views.Primitives
{
    /// <summary>
    /// Interaction logic for CircleView.xaml
    /// </summary>
    public partial class CircleView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircleView"/> class.
        /// </summary>
        public CircleView()
        {
            InitializeComponent();
        }

        private void OnPositionDragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (DataContext is Circle rect)
            {
                rect.Position.X += e.HorizontalChange;
                rect.Position.Y += e.VerticalChange;
            }
        }

        private void OnRadiusDragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (DataContext is Circle rect)
            {
                rect.Radius += e.HorizontalChange;
                if (rect.Radius > 5)
                {
                    rect.Position.X -= e.HorizontalChange / 2d;
                    rect.Position.Y -= e.HorizontalChange / 2d;
                }
            }
        }

        private void OnCircleMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is Circle circle)
            {
                circle.SetSelected();
            }
        }
    }
}
