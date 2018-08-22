using Celestial.UIToolkit.Xaml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Xunit;

namespace Celestial.UIToolkit.Tests.Xaml
{

    public class GridUnitExtensionTests
    {

        private const string ValidDoubleMultiplier = "1.0";
        private const string ValidThicknessMultiplier = "1,2,3,4";
        private const string ValidPointMultiplier = "1.0 2.0";

        [Fact]
        public void ReadsTypeFromPropertyInfo()
        {
            var gridUnitExtension = new GridUnitExtension(ValidThicknessMultiplier);
            var serviceProvider = MockedServiceProvider.CreateForTargetPropertyType<Thickness>();
            object result = gridUnitExtension.ProvideValue(serviceProvider);
            Assert.IsType<Thickness>(result);
        }
        
        [Fact]
        public void ReadsTypeFromDependencyProperties()
        {
            var gridUnitExtension = new GridUnitExtension(ValidThicknessMultiplier);
            var depProp = DependencyProperties.ThicknessProperty; // Use a random dep.prop.
            var serviceProvider = new MockedServiceProvider()
            {
                TargetProperty = depProp
            };

            object result = gridUnitExtension.ProvideValue(serviceProvider);
            Assert.IsType(depProp.PropertyType, result);
        }

        [Fact]
        public void ReadsTypeFromSetters()
        {
            var gridUnitExtension = new GridUnitExtension(ValidPointMultiplier);
            var depProp = DependencyProperties.PointProperty;
            var serviceProvider = new MockedServiceProvider()
            {
                // Use a setter to a random dependency property.
                // What counts is that it has a supported type.
                TargetObject = new Setter(depProp, new Point(1, 1))
            };

            object result = gridUnitExtension.ProvideValue(serviceProvider);
            Assert.IsType(depProp.PropertyType, result);
        }

        [Fact]
        public void UsesTargetTypeProperty()
        {
            // We pass in a property of type A,
            // but set the TargetType to type B.
            // Type B is the expected type of the result.
            var gridUnitExtension = new GridUnitExtension(
                ValidThicknessMultiplier, typeof(Thickness));
            var serviceProvider = MockedServiceProvider.CreateForTargetPropertyType<Point>();

            object result = gridUnitExtension.ProvideValue(serviceProvider);
            Assert.IsType<Thickness>(result);
        }

        [Fact]
        public void DefaultTypeIsDouble()
        {
            var gridUnitExtension = new GridUnitExtension(ValidDoubleMultiplier);
            object result = gridUnitExtension.ProvideValue(MockedServiceProvider.Default);
            Assert.IsType<double>(result);
        }

        [Fact]
        public void ThrowsForUnsetMultiplierString()
        {
            var gridUnitExtension = new GridUnitExtension();
            Exception ex = Record.Exception(
                () => gridUnitExtension.ProvideValue(MockedServiceProvider.Default));
            Assert.IsType<InvalidOperationException>(ex);
        }
        
        [Fact]
        public void ThrowsForUnsupportedType()
        {
            var gridUnitExtension = new GridUnitExtension(
                ValidDoubleMultiplier, typeof(GridUnitExtensionTests));
            Exception ex = Record.Exception(
                () => gridUnitExtension.ProvideValue(MockedServiceProvider.Default));
            Assert.IsType<NotSupportedException>(ex);
        }

        [Fact]
        public void UsesDipMultiplier()
        {
            double dipMultiplier = DipHelper.GetDipMultiplier();
            double input = 10;
            double expectedOutput = input * dipMultiplier;
            var gridUnitExtension = new GridUnitExtension(input.ToString())
            {
                MultiplyWithDip = true,
                GridCellSize = 1        // This must be 1, otherwise the result will be multiplied
            };                          // by 4 (default).
            

            double result = (double)gridUnitExtension.ProvideValue(MockedServiceProvider.Default);
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void GridCellSizeIsChangeable()
        {
            double cellSize = 15;
            double input = 10;
            double expectedOutput = input * cellSize;
            var gridUnitExtension = new GridUnitExtension(input.ToString())
            {
                GridCellSize = cellSize
            };
            
            double result = (double)gridUnitExtension.ProvideValue(MockedServiceProvider.Default);
            Assert.Equal(expectedOutput, result);
        }

        [Theory]
        [ClassData(typeof(SupportedTypesDataSource))]
        public void SupportsTypes(string multiplierString, object expectedResult)
        {
            var gridUnitExtension = new GridUnitExtension(multiplierString, expectedResult.GetType())
            {
                GridCellSize = 4
            };

            object result = gridUnitExtension.ProvideValue(MockedServiceProvider.Default);
            Assert.IsType(expectedResult.GetType(), result);
            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// A data source for the <see cref="GridUnitExtensionTests.SupportsTypes(string, object)"/>
        /// test method.
        /// </summary>
        private class SupportedTypesDataSource : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "2.0", 8d };
                yield return new object[] { "2.0", "8" };      // IConvertible
                yield return new object[] { "2.0", (short)8 }; // IConvertible
                yield return new object[] { "1,2,3,4", new Thickness(4, 8, 12, 16) };
                yield return new object[] { "1,2,3,4", new CornerRadius(4, 8, 12, 16) };
                yield return new object[] { "1,2", new Point(4, 8) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }

}
