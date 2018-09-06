using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{

    public partial class PlaceholderOverlay
    {

        /// <summary>
        /// Identifies the <see cref="Placeholder"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(
                nameof(Placeholder),
                typeof(object),
                typeof(PlaceholderOverlay),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="IsPlaceholderVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPlaceholderVisibleProperty =
            DependencyProperty.Register(
                nameof(IsPlaceholderVisible),
                typeof(bool),
                typeof(PlaceholderOverlay),
                new PropertyMetadata(
                    true,
                    PlaceholderDisplayProperty_Changed));

        /// <summary>
        /// Identifies the <see cref="PlaceholderDisplayType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaceholderDisplayTypeProperty =
            DependencyProperty.Register(
                nameof(PlaceholderDisplayType),
                typeof(PlaceholderDisplayType),
                typeof(PlaceholderOverlay),
                new PropertyMetadata(
                    PlaceholderDisplayType.Floating,
                    PlaceholderDisplayProperty_Changed));

        /// <summary>
        /// Identifies the <see cref="IsPlaceholderHitTestVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPlaceholderHitTestVisibleProperty =
            DependencyProperty.Register(
                nameof(IsPlaceholderHitTestVisible),
                typeof(bool),
                typeof(PlaceholderOverlay),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="FloatingPlaceholderScale"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FloatingPlaceholderScaleProperty =
            DependencyProperty.Register(
                nameof(FloatingPlaceholderScale),
                typeof(double),
                typeof(PlaceholderOverlay),
                new PropertyMetadata(0.8));

        /// <summary>
        /// Gets or sets the placeholder content which is rendered by the control.
        /// </summary>
        [Bindable(true), Category("Content")]
        public object Placeholder
        {
            get { return GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the placeholder is currently displayed.
        /// </summary>
        [Bindable(true), Category("Content")]
        public bool IsPlaceholderVisible
        {
            get { return (bool)GetValue(IsPlaceholderVisibleProperty); }
            set { SetValue(IsPlaceholderVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Controls.PlaceholderDisplayType"/> which is used
        /// for displaying the placeholder.
        /// </summary>
        [Bindable(true), Category("Content")]
        public PlaceholderDisplayType PlaceholderDisplayType
        {
            get { return (PlaceholderDisplayType)GetValue(PlaceholderDisplayTypeProperty); }
            set { SetValue(PlaceholderDisplayTypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the placeholder part of the control
        /// participates in the hit testing system.
        /// </summary>
        [Bindable(true)]
        public bool IsPlaceholderHitTestVisible
        {
            get { return (bool)GetValue(IsPlaceholderHitTestVisibleProperty); }
            set { SetValue(IsPlaceholderHitTestVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the scale of the placeholder when it is floating over the content.
        /// </summary>
        [Bindable(true)]
        public double FloatingPlaceholderScale
        {
            get { return (double)GetValue(FloatingPlaceholderScaleProperty); }
            set { SetValue(FloatingPlaceholderScaleProperty, value); }
        }

    }

}
