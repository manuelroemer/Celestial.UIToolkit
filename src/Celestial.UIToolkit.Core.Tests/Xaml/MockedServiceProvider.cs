using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Celestial.UIToolkit.Tests.Xaml
{

    /// <summary>
    /// A mocked service provider for testing custom markup extensions.
    /// </summary>
    public class MockedServiceProvider : IServiceProvider, IProvideValueTarget
    {

        /// <summary>
        /// Gets a default instance without any property set.
        /// </summary>
        public static MockedServiceProvider Default { get; } = new MockedServiceProvider();

        public object TargetObject { get; set; }

        public object TargetProperty { get; set; }
        
        public object GetService(Type serviceType) => null;

        /// <summary>
        /// Creates a new <see cref="MockedServiceProvider"/> whose <see cref="TargetProperty"/>
        /// is set to a <see cref="PropertyInfo"/> of the specified type <typeparamref name="T"/>.
        /// </summary>
        public static MockedServiceProvider CreateForTargetPropertyType<T>()
        {
            return new MockedServiceProvider()
            {
                TargetProperty = PropertyInfoProvider<T>.GetPropertyInfo()
            };
        }
        
        /// <summary>
        /// Creates <see cref="PropertyInfo"/> objects of a specified type.
        /// </summary>
        private sealed class PropertyInfoProvider<T>
        {
            public T Property { get; set; }
            
            private PropertyInfoProvider() { }

            public static PropertyInfo GetPropertyInfo()
            {
                return new PropertyInfoProvider<T>().GetType().GetProperty(nameof(Property));
            }

        }

    }
    
}
