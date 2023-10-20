using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfPaint.Controls
{
    /// <summary>
    /// Represents a button control, which opens a drop down menu.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Button" />
    public sealed class DropDownButton : Button
    {
        private ContextMenu? _contextMenu;

        /// <summary>
        /// Property for <see cref="Flyout"/>.
        /// </summary>
        public static readonly DependencyProperty FlyoutProperty = DependencyProperty.Register(
            nameof(Flyout),
            typeof(object),
            typeof(DropDownButton),
            new PropertyMetadata(null, OnFlyoutChangedCallback)
        );

        /// <summary>
        /// Property for <see cref="IsDropDownOpen"/>.
        /// </summary>
        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(
            nameof(IsDropDownOpen),
            typeof(bool),
            typeof(DropDownButton),
            new PropertyMetadata(false)
        );

        /// <summary>
        /// Gets or sets the flyout associated with this button.
        /// </summary>
        [Bindable(true)]
        public object? Flyout
        {
            get => GetValue(FlyoutProperty);
            set => SetValue(FlyoutProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the drop-down for a button is currently open.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> if the drop-down is open; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
        [Bindable(true)]
        [Browsable(false)]
        [Category("Appearance")]
        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }

        private static void OnFlyoutChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DropDownButton dropDownButton)
            {
                dropDownButton.OnFlyoutChangedCallback(e.NewValue);
            }
        }

        private void OnFlyoutChangedCallback(object value)
        {
            if (value is ContextMenu contextMenu)
            {
                _contextMenu = contextMenu;
                _contextMenu.Opened += OnContextMenuOpened;
                _contextMenu.Closed += OnContextMenuClosed;
            }
        }

        private void OnContextMenuClosed(object sender, RoutedEventArgs e)
        {
            SetCurrentValue(IsDropDownOpenProperty, false);
        }

        private void OnContextMenuOpened(object sender, RoutedEventArgs e)
        {
            SetCurrentValue(IsDropDownOpenProperty, true);
        }

        protected override void OnClick()
        {
            base.OnClick();

            if (_contextMenu is null)
            {
                return;
            }

            _contextMenu.SetCurrentValue(MinWidthProperty, ActualWidth);
            _contextMenu.HorizontalOffset = -ActualWidth;
            _contextMenu.VerticalOffset = ActualHeight;
            _contextMenu.SetCurrentValue(ContextMenu.PlacementTargetProperty, this);
            _contextMenu.SetCurrentValue(ContextMenu.PlacementProperty, System.Windows.Controls.Primitives.PlacementMode.Right);
            _contextMenu.SetCurrentValue(ContextMenu.IsOpenProperty, true);
        }
    }
}
