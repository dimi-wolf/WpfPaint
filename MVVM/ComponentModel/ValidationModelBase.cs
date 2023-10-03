using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MVVM.ComponentModel
{
    /// <summary>
    /// The base class of a model that supports validation.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.PropertyChangedBase" />
    /// <seealso cref="System.ComponentModel.INotifyDataErrorInfo" />
    public abstract class ValidationModelBase : PropertyChangedBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, HashSet<ValidationResult>> _errorsByProperty = new();

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        public bool HasErrors => _errorsByProperty.Any();

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or <see langword="null" /> or <see cref="System.String.Empty" />, to retrieve entity-level errors.</param>
        /// <returns>
        /// The validation errors for the property or entity.
        /// </returns>
        public IEnumerable GetErrors(string? propertyName)
        {
            // get all erros of the model
            if (propertyName == null)
            {
                return _errorsByProperty.Values.SelectMany(errors => errors).ToList();
            }

            // get errors for a specific property
            if (_errorsByProperty.TryGetValue(propertyName, out HashSet<ValidationResult>? errors))
            {
                return errors.ToList();
            }

            return Enumerable.Empty<string>();
        }

        /// <summary>
        /// Sets the given value of the proprty name.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="field">The field backing field.</param>
        /// <param name="value">The value to be set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected override bool SetValue<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            ArgumentNullException.ThrowIfNull(propertyName, nameof(propertyName));

            bool hasChanged = base.SetValue(ref field, value, propertyName);

            if (hasChanged)
            {
                Validate(value, propertyName);
            }

            return hasChanged;
        }

        protected virtual void Validate<T>(T value, string propertyName)
        {
            var validationContext = new ValidationContext(this, null, null) { MemberName = propertyName };
            var validationResults = new List<ValidationResult>(1);

            if (Validator.TryValidateProperty(value, validationContext, validationResults))
            {
                // property is valid, remove existing errors
                if (_errorsByProperty.ContainsKey(propertyName))
                {
                    _errorsByProperty.Remove(propertyName);
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                }
            }
            else
            {
                // the propery has errors, add them to the dictionary
                if (_errorsByProperty.TryGetValue(propertyName, out HashSet<ValidationResult>? errors))
                {
                    foreach (ValidationResult validationResult in validationResults)
                    {
                        if (!string.IsNullOrEmpty(validationResult.ErrorMessage))
                            errors.Add(validationResult);
                    }
                }
                else
                {
                    _errorsByProperty.Add(propertyName, validationResults.ToHashSet());
                }

                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}
