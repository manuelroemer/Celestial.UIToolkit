using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A control which is used in conjunction with the <see cref="ThemeShadow"/>.
    /// When added to a control's template, it displays a <see cref="Controls.ThemeShadow"/>
    /// which is attached to an element via the <see cref="ThemeShadow.ShadowProperty"/>.
    /// </summary>
    public class ThemeShadowHost : ContentControl
    {

        /// <summary>
        /// Identifies the <see cref="ThemeShadow"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ThemeShadowProperty =
            DependencyProperty.Register(
                nameof(ThemeShadow),
                typeof(ThemeShadow),
                typeof(ThemeShadowHost),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the <see cref="ThemeShadow"/> which is displayed by this host.
        /// </summary>
        public ThemeShadow ThemeShadow
        {
            get { return (ThemeShadow)GetValue(ThemeShadowProperty); }
            set { SetValue(ThemeShadowProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="ThemeShadowTarget"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ThemeShadowTargetProperty =
            DependencyProperty.Register(
                nameof(ThemeShadowTarget),
                typeof(DependencyObject),
                typeof(ThemeShadowHost),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the element whose <see cref="ThemeShadow.ShadowProperty"/> is set.
        /// This is the element from which the <see cref="ThemeShadow"/> property is retrieved.
        /// </summary>
        public DependencyObject ThemeShadowTarget
        {
            get { return (DependencyObject)GetValue(ThemeShadowTargetProperty); }
            set { SetValue(ThemeShadowTargetProperty, value); }
        }

        static ThemeShadowHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ThemeShadowHost), new FrameworkPropertyMetadata(typeof(ThemeShadowHost)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeShadowHost"/> class.
        /// </summary>
        public ThemeShadowHost() { }

        /// <summary>
        /// Returns a string representation of this <see cref="ThemeShadowHost"/>.
        /// </summary>
        /// <returns>A string representing this <see cref="ThemeShadowHost"/>.</returns>
        public override string ToString()
        {
            return $"{nameof(ThemeShadowHost)}: " +
                   $"{nameof(ThemeShadow)}: {ThemeShadow}, " +
                   $"{nameof(ThemeShadowTarget)}: {ThemeShadowTarget}, " +
                   $"{nameof(Content)}: {Content}";
        }

    }

}
