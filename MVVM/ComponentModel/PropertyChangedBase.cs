using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM.ComponentModel
{
    /// <summary>
    /// The base class to notify that a value of a property has changed.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanging" />
    public abstract class PropertyChangedBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <summary>
        /// Occures when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies that a value of a property is changing.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="System.ArgumentNullException">propertyName</exception>
        protected virtual void NotifyPropertyChanging([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

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
        /// Sets the given value of the proprty name.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="field">The field backing field.</param>
        /// <param name="value">The value to be set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="System.ArgumentNullException">propertyName</exception>
        protected virtual bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            // do nothing if the values are equal
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            NotifyPropertyChanging(propertyName);
            field = value;
            NotifyPropertyChanged(propertyName);

            return true;
        }
    }
}
