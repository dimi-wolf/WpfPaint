using System.Windows;
using System.Windows.Controls.Primitives;
using CommunityToolkit.Mvvm.Input;

namespace WpfPaint.Controls
{
    public partial class NumericUpDown : RangeBase
    {
        public static readonly DependencyProperty TickProperty =
            DependencyProperty.Register(
                "Tick",
                typeof(int),
                typeof(NumericUpDown),
                new PropertyMetadata(1));

        public NumericUpDown()
        {
            Minimum = int.MinValue;
            Maximum= int.MaxValue;
            Value = 0;
        }

        public int Tick
        {
            get => (int)GetValue(TickProperty);
            set => SetValue(TickProperty, value);
        }

        [RelayCommand]
        private void Up()
        {
            if (Value < Maximum)
            {
                Value += Tick;
            }
        }

        [RelayCommand]
        private void Down()
        {
            if (Value > Minimum)
            {
                Value -= Tick;
            }
        }
    }
}
