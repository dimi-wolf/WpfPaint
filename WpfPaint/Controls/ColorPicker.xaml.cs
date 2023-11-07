using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPaint.Controls
{
    /// <summary>
    /// Interaction logic for ColorPicker2.xaml
    /// </summary>
    [INotifyPropertyChanged]
    public partial class ColorPicker : UserControl
    {
        private byte _red;
        private byte _green;
        private byte _blue;

        public ColorPicker()
        {
            InitializeComponent();
            DataContext = this;

            var colors = typeof(Colors).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            foreach (System.Reflection.PropertyInfo pi in colors)
            {
                if (pi.GetValue(this, null) is Color c)
                {
                    Colors.Add(pi.Name, c);
                }
            }
        }

        private void NotifyChanged()
        {
            OnPropertyChanged(nameof(RedStart));
            OnPropertyChanged(nameof(RedEnd));
            OnPropertyChanged(nameof(GreenStart));
            OnPropertyChanged(nameof(GreenEnd));
            OnPropertyChanged(nameof(BlueStart));
            OnPropertyChanged(nameof(BlueEnd));
            OnPropertyChanged(nameof(SelectedColor));
            OnPropertyChanged(nameof(HexValue));
        }

        public IDictionary<string, Color> Colors { get; } = new Dictionary<string, Color>();

        public byte Red
        {
            get => _red;
            set
            {
                if (SetProperty(ref _red, value))
                {
                    NotifyChanged();
                }
            }
        }

        public byte Green
        {
            get => _green;
            set
            {
                if (SetProperty(ref _green, value))
                {
                    NotifyChanged();
                }
            }
        }

        public byte Blue
        {
            get => _blue;
            set
            {
                if (SetProperty(ref _blue, value))
                {
                    NotifyChanged();
                }
            }
        }

        public Color RedStart => Color.FromRgb(0, Green, Blue);

        public Color RedEnd => Color.FromRgb(255, Green, Blue);

        public Color GreenStart => Color.FromRgb(Red, 0, Blue);

        public Color GreenEnd => Color.FromRgb(Red, 255, Blue);

        public Color BlueStart => Color.FromRgb(Red, Green, 0);

        public Color BlueEnd => Color.FromRgb(Red, Green, 255);

        public Color SelectedColor
        {
            get => Color.FromRgb(Red, Green, Blue);
            set => SetSelectedColor(value);
        }

        public string HexValue
        {
            get => GetHexValue();
            set => SetHexValue(value);
        }

        private void SetSelectedColor(Color color)
        {
            Red = color.R;
            Green = color.G;
            Blue = color.B;
        }

        private string GetHexValue()
        {
            byte[] bytes = new byte[] { Red, Green, Blue };
            return "#" + Convert.ToHexString(bytes);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private void SetHexValue(string? hexValue)
        {
            if (string.IsNullOrWhiteSpace(hexValue))
            {
                Red = 0;
                Green = 0;
                Blue = 0;
                return;
            }

            try
            {
                if (!hexValue.StartsWith("#", StringComparison.Ordinal))
                {
                    hexValue = "#" + hexValue;
                }
                SelectedColor = (Color)ColorConverter.ConvertFromString(hexValue);
            }
            catch { }
        }
    }
}
