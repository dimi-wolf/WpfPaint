using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfPaint.Model;

namespace WpfPaint.Views.Primitives
{
    /// <summary>
    /// Interaction logic for PolyLineView.xaml
    /// </summary>
    public partial class PolyLineView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolyLineView"/> class.
        /// </summary>
        public PolyLineView()
        {
            InitializeComponent();
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (sender is Thumb t && t.DataContext is Position pos && DataContext is PolyLine polyline)
            {
                pos.X += e.HorizontalChange;
                pos.Y += e.VerticalChange;
                polyline.UpdatePointsCollection();
            }
        }
    }
}

