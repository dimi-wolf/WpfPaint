using MVVM.ComponentModel;

namespace WpfPaint.Model
{
    /// <summary>
    /// Represents a language.
    /// </summary>
    /// <seealso cref="MVVM.ComponentModel.PropertyChangedBase" />
    public class Language : PropertyChangedBase
    {
        private string _code;
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="name">The name.</param>
        public Language(string code, string name)
        {
            _code = code;
            _name = name;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code
        {
            get => _code;
            set => SetValue(ref _code, value);
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
}
