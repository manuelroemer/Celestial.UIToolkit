using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Celestial.UIToolkit.Controls
{

    public partial class ToggleSwitch
    {

        #region Common Properties

        /// <summary>
        /// Identifies the <see cref="IsOn"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register(
                nameof(IsOn),
                typeof(bool),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    false, 
                    (d, e) => ((ToggleSwitch)d).IsOn_Changed(e)));
        
        /// <summary>
        /// Identifies the <see cref="OnCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OnCommandProperty =
            DependencyProperty.Register(
                nameof(OnCommand),
                typeof(ICommand),
                typeof(ToggleSwitch),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="OffCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffCommandProperty =
            DependencyProperty.Register(
                nameof(OffCommand),
                typeof(ICommand),
                typeof(ToggleSwitch),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="OnCommandParameter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OnCommandParameterProperty =
            DependencyProperty.Register(
                nameof(OnCommandParameter),
                typeof(object),
                typeof(ToggleSwitch),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="OffCommandParameter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffCommandParameterProperty =
            DependencyProperty.Register(
                nameof(OffCommandParameter),
                typeof(object),
                typeof(ToggleSwitch),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="ReplaceOnOffContentWithHeader"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ReplaceOnOffContentWithHeaderProperty =
            DependencyProperty.Register(
                nameof(ReplaceOnOffContentWithHeader),
                typeof(bool),
                typeof(ToggleSwitch),
                new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a value indicating whether the switch is currently "On", meaning that
        /// it is toggled.
        /// </summary>
        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets a command which gets executed if the <see cref="ToggleSwitch"/> gets
        /// switched on.
        /// </summary>
        public ICommand OnCommand
        {
            get { return (ICommand)GetValue(OnCommandProperty); }
            set { SetValue(OnCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets a command which gets executed if the <see cref="ToggleSwitch"/> gets
        /// switched off.
        /// </summary>
        public ICommand OffCommand
        {
            get { return (ICommand)GetValue(OffCommandProperty); }
            set { SetValue(OffCommandProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets a parameter which gets passed to the <see cref="OnCommand"/>.
        /// </summary>
        public object OnCommandParameter
        {
            get { return (object)GetValue(OnCommandParameterProperty); }
            set { SetValue(OnCommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets a parameter which gets passed to the <see cref="OffCommand"/>.
        /// </summary>
        public object OffCommandParameter
        {
            get { return (object)GetValue(OffCommandParameterProperty); }
            set { SetValue(OffCommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="OnContent"/> and
        /// <see cref="OffContent"/> get replaced with the <see cref="Header"/>.
        /// </summary>
        public bool ReplaceOnOffContentWithHeader
        {
            get { return (bool)GetValue(ReplaceOnOffContentWithHeaderProperty); }
            set { SetValue(ReplaceOnOffContentWithHeaderProperty, value); }
        }

        #endregion

        #region Template Properties
        
        /// <summary>
        /// Identifies the <see cref="DragOrientation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DragOrientationProperty =
            DependencyProperty.Register(
                nameof(DragOrientation),
                typeof(Orientation),
                typeof(ToggleSwitch),
                new PropertyMetadata(Orientation.Horizontal));

        private static readonly DependencyPropertyKey KnobOffsetPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(KnobOffset),
                typeof(double),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    0d,
                    null,
                    (d, value) => ((ToggleSwitch)d).CoerceKnobOffset((double)value)));

        /// <summary>
        /// Identifies the <see cref="KnobOffset"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty KnobOffsetProperty =
            KnobOffsetPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="OnKnobOffset"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OnKnobOffsetProperty =
            DependencyProperty.Register(
                nameof(OnKnobOffset),
                typeof(double?),
                typeof(ToggleSwitch),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="OffKnobOffset"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffKnobOffsetProperty =
            DependencyProperty.Register(
                nameof(OffKnobOffset),
                typeof(double?),
                typeof(ToggleSwitch),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets whether x or y coordinates are captured when the user drags the
        /// <see cref="ToggleSwitch"/>.
        /// </summary>
        public Orientation DragOrientation
        {
            get { return (Orientation)GetValue(DragOrientationProperty); }
            set { SetValue(DragOrientationProperty, value); }
        }

        /// <summary>
        /// Gets the offset of the <see cref="ToggleSwitch"/>'s knob.
        /// This value is calculated when the user drags the <see cref="ToggleSwitch"/>.
        /// This is intended to be used by templates.
        /// </summary>
        public double KnobOffset
        {
            get { return (double)GetValue(KnobOffsetProperty); }
            private set { SetValue(KnobOffsetPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the value of the <see cref="KnobOffset"/>, when <see cref="IsOn"/>
        /// is false.
        /// This is intended to be used by templates and must explicitly be set in a style to have
        /// an effect.
        /// </summary>
        public double? OnKnobOffset
        {
            get { return (double?)GetValue(OnKnobOffsetProperty); }
            set { SetValue(OnKnobOffsetProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value of the <see cref="KnobOffset"/>, when <see cref="IsOn"/>
        /// is true.
        /// This is intended to be used by templates and must explicitly be set in a style to have
        /// an effect.
        /// </summary>
        public double? OffKnobOffset
        {
            get { return (double?)GetValue(OffKnobOffsetProperty); }
            set { SetValue(OffKnobOffsetProperty, value); }
        }

        #endregion

        #region Content Properties

        /// <summary>
        /// Gets an enumerator on the <see cref="ToggleSwitch"/>'s logical children.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                yield return base.LogicalChildren;
                if (Header != null)
                    yield return Header;
                if (OnContent != null)
                    yield return OnContent;
                if (OffContent != null)
                    yield return OffContent;
            }
        }

        #region Header

        private static readonly DependencyPropertyKey HasHeaderPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasHeader),
                typeof(bool),
                typeof(ToggleSwitch),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="HasHeader"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasHeaderProperty =
            HasHeaderPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="Header"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(
                nameof(Header),
                typeof(object),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    Header_Changed));

        /// <summary>
        /// Identifies the <see cref="HeaderTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register(
                nameof(HeaderTemplate),
                typeof(DataTemplate),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    HeaderTemplate_Changed));

        /// <summary>
        /// Identifies the <see cref="HeaderTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(HeaderTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    HeaderTemplateSelector_Changed));

        /// <summary>
        /// Identifies the <see cref="HeaderStringFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderStringFormatProperty =
            DependencyProperty.Register(
                nameof(HeaderStringFormat),
                typeof(string),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    HeaderStringFormat_Changed));

        /// <summary>
        /// Gets a value indicating whether the <see cref="Header"/> property of this
        /// <see cref="ToggleSwitch"/> holds any content.
        /// </summary>
        [Bindable(false), Browsable(false)]
        public bool HasHeader
        {
            get { return (bool)GetValue(HasHeaderProperty); }
            private set { SetValue(HasHeaderPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the content which is being displayed in the Header of the
        /// <see cref="ToggleSwitch"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
        /// <see cref="Header"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
        /// to provide custom template-selection logic for the <see cref="Header"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplateSelector HeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty); }
            set { SetValue(HeaderTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="Header"/>
        /// content property, if it is displayed as a string.
        /// </summary>
        [Bindable(true), Category("Content")]
        public string HeaderStringFormat
        {
            get { return (string)GetValue(HeaderStringFormatProperty); }
            set { SetValue(HeaderStringFormatProperty, value); }
        }

        private static void Header_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.HasHeader = e.NewValue != null;
            self.OnHeaderChanged(e.OldValue, e.NewValue);
        }

        private static void HeaderTemplate_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.OnHeaderTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        private static void HeaderTemplateSelector_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.OnHeaderTemplateSelectorChanged(
                (DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        private static void HeaderStringFormat_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.OnHeaderStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        /// <summary>
        /// Called when the <see cref="Header"/> property was changed.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="Header"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="Header"/> property.</param>
        protected virtual void OnHeaderChanged(object oldValue, object newValue)
        {
            RemoveLogicalChild(oldValue);
            AddLogicalChild(newValue);
        }

        /// <summary>
        /// Called when the <see cref="HeaderTemplate"/> property was changed.
        /// </summary>
        /// <param name="oldTemplate">The old value of the <see cref="HeaderTemplate"/> property.</param>
        /// <param name="newTemplate">The new value of the <see cref="HeaderTemplate"/> property.</param>
        protected virtual void OnHeaderTemplateChanged(
            DataTemplate oldTemplate, DataTemplate newTemplate)
        { }

        /// <summary>
        /// Called when the <see cref="HeaderTemplateSelector"/> property was changed.
        /// </summary>
        /// <param name="oldSelector">
        /// The old value of the <see cref="HeaderTemplateSelector"/> property.
        /// </param>
        /// <param name="newSelector">
        /// The new value of the <see cref="HeaderTemplateSelector"/> property.
        /// </param>
        protected virtual void OnHeaderTemplateSelectorChanged(
            DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
        { }

        /// <summary>
        /// Called when the <see cref="HeaderStringFormat"/> property was changed.
        /// </summary>
        /// <param name="oldStringFormat">
        /// The old value of the <see cref="HeaderStringFormat"/> property.
        /// </param>
        /// <param name="newStringFormat">
        /// The new value of the <see cref="HeaderStringFormat"/> property.
        /// </param>
        protected virtual void OnHeaderStringFormatChanged(
            string oldStringFormat, string newStringFormat)
        { }

        #endregion

        #region OnContent

        private static readonly DependencyPropertyKey HasOnContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasOnContent),
                typeof(bool),
                typeof(ToggleSwitch),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="HasOnContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasOnContentProperty =
            HasOnContentPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="OnContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OnContentProperty =
            DependencyProperty.Register(
                nameof(OnContent),
                typeof(object),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    OnContent_Changed));

        /// <summary>
        /// Identifies the <see cref="OnContentTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OnContentTemplateProperty =
            DependencyProperty.Register(
                nameof(OnContentTemplate),
                typeof(DataTemplate),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    OnContentTemplate_Changed));

        /// <summary>
        /// Identifies the <see cref="OnContentTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OnContentTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(OnContentTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    OnContentTemplateSelector_Changed));

        /// <summary>
        /// Identifies the <see cref="OnContentStringFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OnContentStringFormatProperty =
            DependencyProperty.Register(
                nameof(OnContentStringFormat),
                typeof(string),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    OnContentStringFormat_Changed));

        /// <summary>
        /// Gets a value indicating whether the <see cref="OnContent"/> property of this
        /// <see cref="ToggleSwitch"/> holds any content.
        /// </summary>
        [Bindable(false), Browsable(false)]
        public bool HasOnContent
        {
            get { return (bool)GetValue(HasOnContentProperty); }
            private set { SetValue(HasOnContentPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the content which is being displayed in the OnContent of the
        /// <see cref="ToggleSwitch"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object OnContent
        {
            get { return (object)GetValue(OnContentProperty); }
            set { SetValue(OnContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
        /// <see cref="OnContent"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplate OnContentTemplate
        {
            get { return (DataTemplate)GetValue(OnContentTemplateProperty); }
            set { SetValue(OnContentTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
        /// to provide custom template-selection logic for the <see cref="OnContent"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplateSelector OnContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(OnContentTemplateSelectorProperty); }
            set { SetValue(OnContentTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="OnContent"/>
        /// content property, if it is displayed as a string.
        /// </summary>
        [Bindable(true), Category("Content")]
        public string OnContentStringFormat
        {
            get { return (string)GetValue(OnContentStringFormatProperty); }
            set { SetValue(OnContentStringFormatProperty, value); }
        }
        
        private static void OnContent_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.HasOnContent = e.NewValue != null;
            self.OnOnContentChanged(e.OldValue, e.NewValue);
        }

        private static void OnContentTemplate_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.OnOnContentTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        private static void OnContentTemplateSelector_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.OnOnContentTemplateSelectorChanged(
                (DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        private static void OnContentStringFormat_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.OnOnContentStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        /// <summary>
        /// Called when the <see cref="OnContent"/> property was changed.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="OnContent"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="OnContent"/> property.</param>
        protected virtual void OnOnContentChanged(object oldValue, object newValue)
        {
            RemoveLogicalChild(oldValue);
            AddLogicalChild(newValue);
        }

        /// <summary>
        /// Called when the <see cref="OnContentTemplate"/> property was changed.
        /// </summary>
        /// <param name="oldTemplate">The old value of the <see cref="OnContentTemplate"/> property.</param>
        /// <param name="newTemplate">The new value of the <see cref="OnContentTemplate"/> property.</param>
        protected virtual void OnOnContentTemplateChanged(
            DataTemplate oldTemplate, DataTemplate newTemplate)
        { }

        /// <summary>
        /// Called when the <see cref="OnContentTemplateSelector"/> property was changed.
        /// </summary>
        /// <param name="oldSelector">
        /// The old value of the <see cref="OnContentTemplateSelector"/> property.
        /// </param>
        /// <param name="newSelector">
        /// The new value of the <see cref="OnContentTemplateSelector"/> property.
        /// </param>
        protected virtual void OnOnContentTemplateSelectorChanged(
            DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
        { }

        /// <summary>
        /// Called when the <see cref="OnContentStringFormat"/> property was changed.
        /// </summary>
        /// <param name="oldStringFormat">
        /// The old value of the <see cref="OnContentStringFormat"/> property.
        /// </param>
        /// <param name="newStringFormat">
        /// The new value of the <see cref="OnContentStringFormat"/> property.
        /// </param>
        protected virtual void OnOnContentStringFormatChanged(
            string oldStringFormat, string newStringFormat)
        { }

        #endregion

        #region OffContent

        private static readonly DependencyPropertyKey HasOffContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasOffContent),
                typeof(bool),
                typeof(ToggleSwitch),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="HasOffContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasOffContentProperty =
            HasOffContentPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="OffContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffContentProperty =
            DependencyProperty.Register(
                nameof(OffContent),
                typeof(object),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    OffContent_Changed));

        /// <summary>
        /// Identifies the <see cref="OffContentTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffContentTemplateProperty =
            DependencyProperty.Register(
                nameof(OffContentTemplate),
                typeof(DataTemplate),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    OffContentTemplate_Changed));

        /// <summary>
        /// Identifies the <see cref="OffContentTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffContentTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(OffContentTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    OffContentTemplateSelector_Changed));

        /// <summary>
        /// Identifies the <see cref="OffContentStringFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffContentStringFormatProperty =
            DependencyProperty.Register(
                nameof(OffContentStringFormat),
                typeof(string),
                typeof(ToggleSwitch),
                new PropertyMetadata(
                    null,
                    OffContentStringFormat_Changed));

        /// <summary>
        /// Gets a value indicating whether the <see cref="OffContent"/> property of this
        /// <see cref="ToggleSwitch"/> holds any content.
        /// </summary>
        [Bindable(false), Browsable(false)]
        public bool HasOffContent
        {
            get { return (bool)GetValue(HasOffContentProperty); }
            private set { SetValue(HasOffContentPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the content which is being displayed in the OffContent of the
        /// <see cref="ToggleSwitch"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object OffContent
        {
            get { return (object)GetValue(OffContentProperty); }
            set { SetValue(OffContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
        /// <see cref="OffContent"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplate OffContentTemplate
        {
            get { return (DataTemplate)GetValue(OffContentTemplateProperty); }
            set { SetValue(OffContentTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
        /// to provide custom template-selection logic for the <see cref="OffContent"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplateSelector OffContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(OffContentTemplateSelectorProperty); }
            set { SetValue(OffContentTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="OffContent"/>
        /// content property, if it is displayed as a string.
        /// </summary>
        [Bindable(true), Category("Content")]
        public string OffContentStringFormat
        {
            get { return (string)GetValue(OffContentStringFormatProperty); }
            set { SetValue(OffContentStringFormatProperty, value); }
        }
        
        private static void OffContent_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.HasOffContent = e.NewValue != null;
            self.OnOffContentChanged(e.OldValue, e.NewValue);
        }

        private static void OffContentTemplate_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.OnOffContentTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        private static void OffContentTemplateSelector_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.OnOffContentTemplateSelectorChanged(
                (DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        private static void OffContentStringFormat_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ToggleSwitch)d;
            self.OnOffContentStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        /// <summary>
        /// Called when the <see cref="OffContent"/> property was changed.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="OffContent"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="OffContent"/> property.</param>
        protected virtual void OnOffContentChanged(object oldValue, object newValue)
        {
            RemoveLogicalChild(oldValue);
            AddLogicalChild(newValue);
        }

        /// <summary>
        /// Called when the <see cref="OffContentTemplate"/> property was changed.
        /// </summary>
        /// <param name="oldTemplate">The old value of the <see cref="OffContentTemplate"/> property.</param>
        /// <param name="newTemplate">The new value of the <see cref="OffContentTemplate"/> property.</param>
        protected virtual void OnOffContentTemplateChanged(
            DataTemplate oldTemplate, DataTemplate newTemplate)
        { }

        /// <summary>
        /// Called when the <see cref="OffContentTemplateSelector"/> property was changed.
        /// </summary>
        /// <param name="oldSelector">
        /// The old value of the <see cref="OffContentTemplateSelector"/> property.
        /// </param>
        /// <param name="newSelector">
        /// The new value of the <see cref="OffContentTemplateSelector"/> property.
        /// </param>
        protected virtual void OnOffContentTemplateSelectorChanged(
            DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
        { }

        /// <summary>
        /// Called when the <see cref="OffContentStringFormat"/> property was changed.
        /// </summary>
        /// <param name="oldStringFormat">
        /// The old value of the <see cref="OffContentStringFormat"/> property.
        /// </param>
        /// <param name="newStringFormat">
        /// The new value of the <see cref="OffContentStringFormat"/> property.
        /// </param>
        protected virtual void OnOffContentStringFormatChanged(
            string oldStringFormat, string newStringFormat)
        { }

        #endregion

        #endregion

    }

}
