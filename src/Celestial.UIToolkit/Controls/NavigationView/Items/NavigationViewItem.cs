using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Represents the container for a generic navigation item in a <see cref="NavigationView"/>.
    /// </summary>
    /// <remarks>
    /// This class is inheriting from <see cref="ListViewItem"/> since the
    /// <see cref="NavigationView"/> is using a <see cref="ListView"/> to display its menu items.
    /// To ensure that a custom template will still display these items correctly, it should use
    /// a <see cref="ListView"/> aswell.
    /// </remarks>
    public class NavigationViewItem : ListViewItem
    {
        
        private static readonly DependencyPropertyKey HasIconPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasIcon),
                typeof(bool),
                typeof(NavigationViewItem),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="HasIcon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasIconProperty =
            HasIconPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="Icon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(Icon),
                typeof(object),
                typeof(NavigationViewItem),
                new PropertyMetadata(
                    null,
                    Icon_Changed));

        /// <summary>
        /// Identifies the <see cref="IconTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconTemplateProperty =
            DependencyProperty.Register(
                nameof(IconTemplate),
                typeof(DataTemplate),
                typeof(NavigationViewItem),
                new PropertyMetadata(
                    null,
                    IconTemplate_Changed));

        /// <summary>
        /// Identifies the <see cref="IconTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(IconTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(NavigationViewItem),
                new PropertyMetadata(
                    null,
                    IconTemplateSelector_Changed));

        /// <summary>
        /// Identifies the <see cref="IconStringFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconStringFormatProperty =
            DependencyProperty.Register(
                nameof(IconStringFormat),
                typeof(string),
                typeof(NavigationViewItem),
                new PropertyMetadata(
                    null,
                    IconStringFormat_Changed));

        /// <summary>
        /// Gets a value indicating whether the <see cref="Icon"/> property of this
        /// <see cref="NavigationViewItem"/> holds any content.
        /// </summary>
        [Bindable(false), Browsable(false)]
        public bool HasIcon
        {
            get { return (bool)GetValue(HasIconProperty); }
            private set { SetValue(HasIconPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the content which is being displayed in the Icon of the
        /// <see cref="NavigationViewItem"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
        /// <see cref="Icon"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
        /// to provide custom template-selection logic for the <see cref="Icon"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplateSelector IconTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(IconTemplateSelectorProperty); }
            set { SetValue(IconTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="Icon"/>
        /// content property, if it is displayed as a string.
        /// </summary>
        [Bindable(true), Category("Content")]
        public string IconStringFormat
        {
            get { return (string)GetValue(IconStringFormatProperty); }
            set { SetValue(IconStringFormatProperty, value); }
        }

        /// <summary>
        /// Gets an enumerator on the <see cref="NavigationViewItem"/>'s logical children.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                yield return base.LogicalChildren;
                if (Icon != null)
                    yield return Icon;
            }
        }

        static NavigationViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(NavigationViewItem), 
                new FrameworkPropertyMetadata(typeof(NavigationViewItem)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewItem"/> class.
        /// </summary>
        public NavigationViewItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewItem"/> class
        /// with the specified <paramref name="content"/>.
        /// </summary>
        /// <param name="content">The item's content.</param>
        public NavigationViewItem(object content)
            : this(content, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewItem"/> class
        /// with the specified <paramref name="content"/> and <paramref name="icon"/>.
        /// </summary>
        /// <param name="content">The item's content.</param>
        /// <param name="icon">The item's icon.</param>
        public NavigationViewItem(object content, object icon)
        {
            Content = content;
            Icon = icon;
        }

        private static void Icon_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationViewItem)d;
            self.HasIcon = e.NewValue != null;
            self.OnIconChanged(e.OldValue, e.NewValue);
        }

        private static void IconTemplate_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationViewItem)d;
            self.OnIconTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        private static void IconTemplateSelector_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationViewItem)d;
            self.OnIconTemplateSelectorChanged(
                (DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        private static void IconStringFormat_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationViewItem)d;
            self.OnIconStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        /// <summary>
        /// Called when the <see cref="Icon"/> property was changed.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="Icon"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="Icon"/> property.</param>
        protected virtual void OnIconChanged(object oldValue, object newValue)
        {
            RemoveLogicalChild(oldValue);
            AddLogicalChild(newValue);
        }

        /// <summary>
        /// Called when the <see cref="IconTemplate"/> property was changed.
        /// </summary>
        /// <param name="oldTemplate">The old value of the <see cref="IconTemplate"/> property.</param>
        /// <param name="newTemplate">The new value of the <see cref="IconTemplate"/> property.</param>
        protected virtual void OnIconTemplateChanged(
            DataTemplate oldTemplate, DataTemplate newTemplate)
        { }

        /// <summary>
        /// Called when the <see cref="IconTemplateSelector"/> property was changed.
        /// </summary>
        /// <param name="oldSelector">
        /// The old value of the <see cref="IconTemplateSelector"/> property.
        /// </param>
        /// <param name="newSelector">
        /// The new value of the <see cref="IconTemplateSelector"/> property.
        /// </param>
        protected virtual void OnIconTemplateSelectorChanged(
            DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
        { }

        /// <summary>
        /// Called when the <see cref="IconStringFormat"/> property was changed.
        /// </summary>
        /// <param name="oldStringFormat">
        /// The old value of the <see cref="IconStringFormat"/> property.
        /// </param>
        /// <param name="newStringFormat">
        /// The new value of the <see cref="IconStringFormat"/> property.
        /// </param>
        protected virtual void OnIconStringFormatChanged(
            string oldStringFormat, string newStringFormat)
        { }

        /// <summary>
        /// Returns a string representation of this <see cref="NavigationViewItem"/>.
        /// </summary>
        /// <returns>
        /// A string representing this <see cref="NavigationViewItem"/>.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(NavigationViewItem)}: {Content}";
        }

    }

}
