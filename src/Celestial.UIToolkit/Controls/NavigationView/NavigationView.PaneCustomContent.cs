using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {

        private static readonly DependencyPropertyKey HasPaneCustomContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasPaneCustomContent),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="HasPaneCustomContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasPaneCustomContentProperty =
            HasPaneCustomContentPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="PaneCustomContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneCustomContentProperty =
            DependencyProperty.Register(
                nameof(PaneCustomContent),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneCustomContent_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneCustomContentTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneCustomContentTemplateProperty =
            DependencyProperty.Register(
                nameof(PaneCustomContentTemplate),
                typeof(DataTemplate),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneCustomContentTemplate_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneCustomContentTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneCustomContentTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(PaneCustomContentTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneCustomContentTemplateSelector_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneCustomContentStringFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneCustomContentStringFormatProperty =
            DependencyProperty.Register(
                nameof(PaneCustomContentStringFormat),
                typeof(string),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    PaneCustomContentStringFormat_Changed));

        /// <summary>
        /// Gets a value indicating whether the <see cref="PaneCustomContent"/> property of this
        /// <see cref="NavigationView"/> holds any content.
        /// </summary>
        [Bindable(false), Browsable(false)]
        public bool HasPaneCustomContent
        {
            get { return (bool)GetValue(HasPaneCustomContentProperty); }
            private set { SetValue(HasPaneCustomContentPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the content which is being displayed in the PaneCustomContent of the
        /// <see cref="NavigationView"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object PaneCustomContent
        {
            get { return (object)GetValue(PaneCustomContentProperty); }
            set { SetValue(PaneCustomContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
        /// <see cref="PaneCustomContent"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplate PaneCustomContentTemplate
        {
            get { return (DataTemplate)GetValue(PaneCustomContentTemplateProperty); }
            set { SetValue(PaneCustomContentTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
        /// to provide custom template-selection logic for the <see cref="PaneCustomContent"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplateSelector PaneCustomContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(PaneCustomContentTemplateSelectorProperty); }
            set { SetValue(PaneCustomContentTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="PaneCustomContent"/>
        /// content property, if it is displayed as a string.
        /// </summary>
        [Bindable(true), Category("Content")]
        public string PaneCustomContentStringFormat
        {
            get { return (string)GetValue(PaneCustomContentStringFormatProperty); }
            set { SetValue(PaneCustomContentStringFormatProperty, value); }
        }
        
        private static void PaneCustomContent_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.HasPaneCustomContent = e.NewValue != null;
            self.OnPaneCustomContentChanged(e.OldValue, e.NewValue);
        }

        private static void PaneCustomContentTemplate_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.OnPaneCustomContentTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        private static void PaneCustomContentTemplateSelector_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.OnPaneCustomContentTemplateSelectorChanged(
                (DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        private static void PaneCustomContentStringFormat_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.OnPaneCustomContentStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        /// <summary>
        /// Called when the <see cref="PaneCustomContent"/> property was changed.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="PaneCustomContent"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="PaneCustomContent"/> property.</param>
        protected virtual void OnPaneCustomContentChanged(object oldValue, object newValue)
        {
            RemoveLogicalChild(oldValue);
            AddLogicalChild(newValue);
        }

        /// <summary>
        /// Called when the <see cref="PaneCustomContentTemplate"/> property was changed.
        /// </summary>
        /// <param name="oldTemplate">The old value of the <see cref="PaneCustomContentTemplate"/> property.</param>
        /// <param name="newTemplate">The new value of the <see cref="PaneCustomContentTemplate"/> property.</param>
        protected virtual void OnPaneCustomContentTemplateChanged(
            DataTemplate oldTemplate, DataTemplate newTemplate)
        { }

        /// <summary>
        /// Called when the <see cref="PaneCustomContentTemplateSelector"/> property was changed.
        /// </summary>
        /// <param name="oldSelector">
        /// The old value of the <see cref="PaneCustomContentTemplateSelector"/> property.
        /// </param>
        /// <param name="newSelector">
        /// The new value of the <see cref="PaneCustomContentTemplateSelector"/> property.
        /// </param>
        protected virtual void OnPaneCustomContentTemplateSelectorChanged(
            DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
        { }

        /// <summary>
        /// Called when the <see cref="PaneCustomContentStringFormat"/> property was changed.
        /// </summary>
        /// <param name="oldStringFormat">
        /// The old value of the <see cref="PaneCustomContentStringFormat"/> property.
        /// </param>
        /// <param name="newStringFormat">
        /// The new value of the <see cref="PaneCustomContentStringFormat"/> property.
        /// </param>
        protected virtual void OnPaneCustomContentStringFormatChanged(
            string oldStringFormat, string newStringFormat)
        { }

    }

}
