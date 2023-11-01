using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace WpfPaint.Model
{
    /// <summary>
    /// The rectangle primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.Model.PrimitiveBase" />
    public partial class Rectangle : PrimitiveBase
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [ObservableProperty]
        private Position _position = new();

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ObservableProperty]
        [Range(0, double.MaxValue, ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.ErrorOnlyPositiveValuesAllowed))]
        private double _width;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [ObservableProperty]
        [Range(0, double.MaxValue, ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.ErrorOnlyPositiveValuesAllowed))]
        private double _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle()
            : base(WeakReferenceMessenger.Default)
        {
            Name = Resources.Strings.Rectangle;
        }
    }
}
