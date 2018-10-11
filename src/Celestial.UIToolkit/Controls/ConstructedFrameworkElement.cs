using System;
using System.Collections;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A special <see cref="FrameworkElement"/> which provides sub-classes with a "Child" 
    /// property, similar to WPF's <see cref="System.Windows.Controls.Decorator"/>.
    /// This allows sub-classes to automatically build a fixed element tree, without the user
    /// being able to change it.
    /// Hence the term "Constructed", because sub-classes are supposed to construct the element 
    /// tree.
    /// </summary>
    public class ConstructedFrameworkElement : FrameworkElement
    {

        private UIElement _child;

        /// <summary>
        /// Gets or sets a single child of the <see cref="IconElement"/>.
        /// This child should be used to display other controls which ultimately render 
        /// the icon's content.
        /// </summary>
        protected UIElement Child
        {
            get { return _child; }
            set
            {
                if (_child != value)
                {
                    RemoveVisualChild(_child);
                    RemoveLogicalChild(_child);
                    AddVisualChild(value);
                    AddLogicalChild(value);
                    _child = value;
                    
                    InvalidateMeasure();
                }
            }
        }
        
        /// <summary>
        /// Gets an enumerator over the control's logical children.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (Child != null)
                {
                    yield return Child;
                }
            }
        }

        /// <summary>
        /// Gets the number of visual child elements within this element.
        /// </summary>
        protected override int VisualChildrenCount => Child == null ? 0 : 1;

        /// <summary>
        /// If <paramref name="index"/> is 0 and <see cref="Child"/> is not null,
        /// this method returns <see cref="Child"/>.
        /// </summary>
        /// <param name="index">The index. Must be 0 to not throw an exception.</param>
        /// <returns>
        /// <see cref="Child"/>, if the conditions are met.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <see cref="Child"/> is null, or if <paramref name="index"/> is not 0.
        /// </exception>
        protected override Visual GetVisualChild(int index)
        {
            if (Child == null || index != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            return _child;
        }

        /// <summary>
        /// Measures the size of the inner <see cref="Child"/>.
        /// </summary>
        /// <param name="availableSize">The available size.</param>
        /// <returns>
        /// The size required by the <see cref="Child"/> or (0;0).
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Child != null)
            {
                Child.Measure(availableSize);
                return Child.DesiredSize;
            }
            else
            {
                return new Size();
            }
        }

        /// <summary>
        /// Positions the inner <see cref="Child"/>.
        /// </summary>
        /// <param name="finalSize">The final available size.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Child != null)
            {
                Child.Arrange(new Rect(finalSize));
            }
            return finalSize;
        }

    }

}
