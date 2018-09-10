using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ControlGallery.Controls
{

    /// <summary>
    /// A custom control which is used by the gallery to display a feature of controls.
    /// It has a header, a content to display the control and also a special pane in which
    /// controls that change properties of the displayed control can be placed.
    /// </summary>
    public class ControlPresenter : HeaderedContentControl
    {

        /// <summary>
        /// Identifies the <see cref="Xaml"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty XamlProperty =
            DependencyProperty.Register(
                nameof(Xaml),
                typeof(string),
                typeof(ControlPresenter),
                new PropertyMetadata(null));
        
        /// <summary>
        /// Identifies the <see cref="Description"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                nameof(Description),
                typeof(string),
                typeof(ControlPresenter),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a description of the displayed content.
        /// </summary>
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets a string which represents the XAML code required to display
        /// and create the presented control content.
        /// </summary>
        public string Xaml
        {
            get { return (string)GetValue(XamlProperty); }
            set { SetValue(XamlProperty, value); }
        }

        private static readonly DependencyPropertyKey HasOptionsPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasOptions),
                typeof(bool),
                typeof(ControlPresenter),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="HasOptions"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasOptionsProperty =
            HasOptionsPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="Options"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register(
                nameof(Options),
                typeof(object),
                typeof(ControlPresenter),
                new PropertyMetadata(
                    null,
                    Options_Changed));
        
        /// <summary>
        /// Gets a value indicating whether the <see cref="Options"/> property of this
        /// <see cref="ControlPresenter"/> holds any content.
        /// </summary>
        [Bindable(false), Browsable(false)]
        public bool HasOptions
        {
            get { return (bool)GetValue(HasOptionsProperty); }
            private set { SetValue(HasOptionsPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the content which is being displayed in the Options of the
        /// <see cref="ControlPresenter"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object Options
        {
            get { return (object)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }
        
        /// <summary>
        /// Gets an enumerator on the <see cref="ControlPresenter"/>'s logical children.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                yield return base.LogicalChildren;
                if (Options != null)
                    yield return Options;
            }
        }

        static ControlPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ControlPresenter), new FrameworkPropertyMetadata(typeof(ControlPresenter)));
        }

        private static void Options_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ControlPresenter)d;
            self.HasOptions = e.NewValue != null;
            self.OnOptionsChanged(e.OldValue, e.NewValue);
        }
        
        /// <summary>
        /// Called when the <see cref="Options"/> property was changed.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="Options"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="Options"/> property.</param>
        protected virtual void OnOptionsChanged(object oldValue, object newValue)
        {
            RemoveLogicalChild(oldValue);
            AddLogicalChild(newValue);
        }
        
    }

}
