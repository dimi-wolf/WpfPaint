using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using MVVM.ComponentModel;

namespace MVVM.Behaviors
{
    /// <summary>
    /// Controls the lifecycle of a view model.
    /// </summary>
    /// <seealso cref="Microsoft.Xaml.Behaviors.Behavior&lt;System.Windows.Controls.Control&gt;" />
    public class LifecycleBehavior : Behavior<Control>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
        }

        /// <summary>
        /// Handles the Loaded event of the AssociatedObject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AssociatedObject.DataContext is ViewModelBase vm)
            {
                _ = vm.OnLoadingAsync();
            }
        }

        /// <summary>
        /// Handles the Unloaded event of the AssociatedObject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AssociatedObject_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AssociatedObject.DataContext is ViewModelBase vm)
            {
                _ = vm.OnUnloadingAsync();
            }
        }
    }
}
