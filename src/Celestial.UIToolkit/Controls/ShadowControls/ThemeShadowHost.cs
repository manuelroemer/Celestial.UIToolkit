using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A control which is used in conjunction with the <see cref="ThemeShadow"/>.
    /// When added to a control's template, a <see cref="ThemeShadow"/> can use this host
    /// to display itself.
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

        static ThemeShadowHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ThemeShadowHost), new FrameworkPropertyMetadata(typeof(ThemeShadowHost)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeShadowHost"/> class.
        /// </summary>
        public ThemeShadowHost() { }

    }

}
