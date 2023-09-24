using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfPaint.Model
{
    /// <summary>
    /// An in memory collection for the drawn objects.
    /// </summary>
    public class ObjectsCollection
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<ObjectTypes, Type> _objectTypeLookup = new()
        {
            [ObjectTypes.Rectangle] = typeof(Rectangle),
            [ObjectTypes.Circle] = typeof(Circle),
            [ObjectTypes.PolyLine] = typeof(PolyLine)
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectsCollection"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public ObjectsCollection(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets all drawn objects.
        /// </summary>
        public ObservableCollection<object> Objects { get; } = new();

        /// <summary>
        /// Adds the new object.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        public void AddNewObject(ObjectTypes objectType)
        {
            if (_objectTypeLookup.TryGetValue(objectType, out var type))
            {
                var instance = _serviceProvider.GetService(type);

                if (instance != null)
                {
                    Objects.Add(instance);
                }
            }
        }
    }
}
