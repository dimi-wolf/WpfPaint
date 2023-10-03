using System.Windows.Data;

namespace WpfPaint.Localization
{
    /// <summary>
    /// A binding extension to support dynamic localization.
    /// </summary>
    /// <seealso cref="System.Windows.Data.Binding" />
    public class LocalizationExtension : Binding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationExtension"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LocalizationExtension(string name)
            : base("[" + name + "]")
        {
            Mode = BindingMode.OneWay;
            Source = LocalizationSource.Instance;
            FallbackValue = name;
        }
    }
}
