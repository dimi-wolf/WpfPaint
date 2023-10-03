﻿using WpfPaint.Messaging;

namespace WpfPaint.Model
{
    /// <summary>
    /// The circle primitive.
    /// </summary>
    /// <seealso cref="WpfPaint.Model.PrimitiveBase" />
    public class Circle : PrimitiveBase
    {
        private double _radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public Circle(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
            Name = "Kreis";
            Radius = 100;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Position Position { get; } = new Position();

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        public double Radius
        {
            get => _radius;
            set => SetValue(ref _radius, CheckedValue(value));
        }

        private static double CheckedValue(double value) => value < 0 ? 0 : value;
    }
}
