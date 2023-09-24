using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfPaint.MVVM
{
    /// <summary>
    /// The base class to notify that a value of a property has changed.
    /// </summary>
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object?> _propertyValues = new();

        /// <summary>
        /// Occures when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies that a value of a property has changed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets the value of the given proprty name.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="propertyName">The proprty name.</param>
        /// <returns>
        /// The value of the given proprty name or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual T? GetValue<T>([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            if (_propertyValues.TryGetValue(propertyName, out var value))
            {
                return (T?)value;
            }

            return default;
        }

        /// <summary>
        /// Sets the given value of the proprty name.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value to be set.</param>
        /// <param name="propertyName">The property name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual void SetValue<T>(T? value, [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            _propertyValues[propertyName] = value;
            NotifyPropertyChanged(propertyName);
        }
    }
}
