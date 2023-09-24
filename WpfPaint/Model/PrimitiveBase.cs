using WpfPaint.MVVM;

namespace WpfPaint.Model
{
    /// <summary>
    /// The base class of an primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.PropertyChangedBase" />
    public abstract class PrimitiveBase : PropertyChangedBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get => GetValue<string>() ?? string.Empty;
            set => SetValue(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the controls should be shown.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the controls are shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowControls
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
    }
}
