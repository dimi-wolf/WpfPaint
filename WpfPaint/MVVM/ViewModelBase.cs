﻿using System.Threading.Tasks;

namespace WpfPaint.MVVM
{
    /// <summary>
    /// The base class of a view model.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.PropertyChangedBase" />
    public abstract class ViewModelBase : PropertyChangedBase
    {
        /// <summary>
        /// Gets a value indicating whether the view model is loaded.
        /// </summary>
        public bool IsLoaded
        {
            get => GetValue<bool>();
            protected set => SetValue(value);
        }

        /// <summary>
        /// Called when the view model is loading.
        /// </summary>
        /// <returns>
        /// An awaitable <see cref="Task"/>.
        /// </returns>
        protected internal virtual Task OnLoadingAsync()
        {
            IsLoaded = true;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when the view model is unloading.
        /// </summary>
        /// <returns>
        /// An awaitable <see cref="Task"/>.
        /// </returns>
        protected internal virtual Task OnUnloadingAsync()
        {
            IsLoaded = false;
            return Task.CompletedTask;
        }
    }
}
