using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {
        
        private static readonly DependencyPropertyKey HasPaneFooterPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasPaneFooter),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="HasPaneFooter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasPaneFooterProperty =
            HasPaneFooterPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="PaneFooter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneFooterProperty =
            DependencyProperty.Register(
                nameof(PaneFooter),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneFooter_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneFooterTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneFooterTemplateProperty =
            DependencyProperty.Register(
                nameof(PaneFooterTemplate),
                typeof(DataTemplate),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneFooterTemplate_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneFooterTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneFooterTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(PaneFooterTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneFooterTemplateSelector_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneFooterStringFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneFooterStringFormatProperty =
            DependencyProperty.Register(
                nameof(PaneFooterStringFormat),
                typeof(string),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneFooterStringFormat_Changed));

        /// <summary>
        /// Gets a value indicating whether the <see cref="PaneFooter"/> property of this
        /// <see cref="NavigationView"/> holds any content.
        /// </summary>
        [Bindable(false), Browsable(false)]
        public bool HasPaneFooter
        {
            get { return (bool)GetValue(HasPaneFooterProperty); }
            private set { SetValue(HasPaneFooterPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the content which is being displayed in the PaneFooter of the
        /// <see cref="NavigationView"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object PaneFooter
        {
            get { return (object)GetValue(PaneFooterProperty); }
            set { SetValue(PaneFooterProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
        /// <see cref="PaneFooter"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplate PaneFooterTemplate
        {
            get { return (DataTemplate)GetValue(PaneFooterTemplateProperty); }
            set { SetValue(PaneFooterTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
        /// to provide custom template-selection logic for the <see cref="PaneFooter"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplateSelector PaneFooterTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(PaneFooterTemplateSelectorProperty); }
            set { SetValue(PaneFooterTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="PaneFooter"/>
        /// content property, if it is displayed as a string.
        /// </summary>
        [Bindable(true), Category("Content")]
        public string PaneFooterStringFormat
        {
            get { return (string)GetValue(PaneFooterStringFormatProperty); }
            set { SetValue(PaneFooterStringFormatProperty, value); }
        }

        private static void PaneFooter_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.HasPaneFooter = e.NewValue != null;
            self.OnPaneFooterChanged(e.OldValue, e.NewValue);
        }

        private static void PaneFooterTemplate_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.OnPaneFooterTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        private static void PaneFooterTemplateSelector_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.OnPaneFooterTemplateSelectorChanged(
                (DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        private static void PaneFooterStringFormat_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.OnPaneFooterStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        /// <summary>
        /// Called when the <see cref="PaneFooter"/> property was changed.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="PaneFooter"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="PaneFooter"/> property.</param>
        protected virtual void OnPaneFooterChanged(object oldValue, object newValue)
        {
            RemoveLogicalChild(oldValue);
            AddLogicalChild(newValue);
        }

        /// <summary>
        /// Called when the <see cref="PaneFooterTemplate"/> property was changed.
        /// </summary>
        /// <param name="oldTemplate">The old value of the <see cref="PaneFooterTemplate"/> property.</param>
        /// <param name="newTemplate">The new value of the <see cref="PaneFooterTemplate"/> property.</param>
        protected virtual void OnPaneFooterTemplateChanged(
            DataTemplate oldTemplate, DataTemplate newTemplate)
        { }

        /// <summary>
        /// Called when the <see cref="PaneFooterTemplateSelector"/> property was changed.
        /// </summary>
        /// <param name="oldSelector">
        /// The old value of the <see cref="PaneFooterTemplateSelector"/> property.
        /// </param>
        /// <param name="newSelector">
        /// The new value of the <see cref="PaneFooterTemplateSelector"/> property.
        /// </param>
        protected virtual void OnPaneFooterTemplateSelectorChanged(
            DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
        { }

        /// <summary>
        /// Called when the <see cref="PaneFooterStringFormat"/> property was changed.
        /// </summary>
        /// <param name="oldStringFormat">
        /// The old value of the <see cref="PaneFooterStringFormat"/> property.
        /// </param>
        /// <param name="newStringFormat">
        /// The new value of the <see cref="PaneFooterStringFormat"/> property.
        /// </param>
        protected virtual void OnPaneFooterStringFormatChanged(
            string oldStringFormat, string newStringFormat)
        { }

    }

}
