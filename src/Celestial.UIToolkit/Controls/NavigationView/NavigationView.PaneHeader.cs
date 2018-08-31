using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {

        private static readonly DependencyPropertyKey HasPaneHeaderPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasPaneHeader),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="HasPaneHeader"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasPaneHeaderProperty =
            HasPaneHeaderPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="PaneHeader"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneHeaderProperty =
            DependencyProperty.Register(
                nameof(PaneHeader),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneHeader_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneHeaderTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneHeaderTemplateProperty =
            DependencyProperty.Register(
                nameof(PaneHeaderTemplate),
                typeof(DataTemplate),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneHeaderTemplate_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneHeaderTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneHeaderTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(PaneHeaderTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneHeaderTemplateSelector_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneHeaderStringFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneHeaderStringFormatProperty =
            DependencyProperty.Register(
                nameof(PaneHeaderStringFormat),
                typeof(string),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneHeaderStringFormat_Changed));

        /// <summary>
        /// Gets a value indicating whether the <see cref="PaneHeader"/> property of this
        /// <see cref="NavigationView"/> holds any content.
        /// </summary>
        [Bindable(false), Browsable(false)]
        public bool HasPaneHeader
        {
            get { return (bool)GetValue(HasPaneHeaderProperty); }
            private set { SetValue(HasPaneHeaderPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the content which is being displayed in the PaneHeader of the
        /// <see cref="NavigationView"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object PaneHeader
        {
            get { return (object)GetValue(PaneHeaderProperty); }
            set { SetValue(PaneHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
        /// <see cref="PaneHeader"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplate PaneHeaderTemplate
        {
            get { return (DataTemplate)GetValue(PaneHeaderTemplateProperty); }
            set { SetValue(PaneHeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
        /// to provide custom template-selection logic for the <see cref="PaneHeader"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplateSelector PaneHeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(PaneHeaderTemplateSelectorProperty); }
            set { SetValue(PaneHeaderTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="PaneHeader"/>
        /// content property, if it is displayed as a string.
        /// </summary>
        [Bindable(true), Category("Content")]
        public string PaneHeaderStringFormat
        {
            get { return (string)GetValue(PaneHeaderStringFormatProperty); }
            set { SetValue(PaneHeaderStringFormatProperty, value); }
        }

        private static void PaneHeader_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.HasPaneHeader = e.NewValue != null;
            self.OnPaneHeaderChanged(e.OldValue, e.NewValue);
        }

        private static void PaneHeaderTemplate_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.OnPaneHeaderTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        private static void PaneHeaderTemplateSelector_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.OnPaneHeaderTemplateSelectorChanged(
                (DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        private static void PaneHeaderStringFormat_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.OnPaneHeaderStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        /// <summary>
        /// Called when the <see cref="PaneHeader"/> property was changed.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="PaneHeader"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="PaneHeader"/> property.</param>
        protected virtual void OnPaneHeaderChanged(object oldValue, object newValue)
        {
            RemoveLogicalChild(oldValue);
            AddLogicalChild(newValue);
        }

        /// <summary>
        /// Called when the <see cref="PaneHeaderTemplate"/> property was changed.
        /// </summary>
        /// <param name="oldTemplate">The old value of the <see cref="PaneHeaderTemplate"/> property.</param>
        /// <param name="newTemplate">The new value of the <see cref="PaneHeaderTemplate"/> property.</param>
        protected virtual void OnPaneHeaderTemplateChanged(
            DataTemplate oldTemplate, DataTemplate newTemplate)
        { }

        /// <summary>
        /// Called when the <see cref="PaneHeaderTemplateSelector"/> property was changed.
        /// </summary>
        /// <param name="oldSelector">
        /// The old value of the <see cref="PaneHeaderTemplateSelector"/> property.
        /// </param>
        /// <param name="newSelector">
        /// The new value of the <see cref="PaneHeaderTemplateSelector"/> property.
        /// </param>
        protected virtual void OnPaneHeaderTemplateSelectorChanged(
            DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
        { }

        /// <summary>
        /// Called when the <see cref="PaneHeaderStringFormat"/> property was changed.
        /// </summary>
        /// <param name="oldStringFormat">
        /// The old value of the <see cref="PaneHeaderStringFormat"/> property.
        /// </param>
        /// <param name="newStringFormat">
        /// The new value of the <see cref="PaneHeaderStringFormat"/> property.
        /// </param>
        protected virtual void OnPaneHeaderStringFormatChanged(
            string oldStringFormat, string newStringFormat)
        { }

    }
}
