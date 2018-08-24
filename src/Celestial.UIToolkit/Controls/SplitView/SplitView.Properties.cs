using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Celestial.UIToolkit.Controls
{

    public partial class SplitView
    {

        /// <summary>
        /// Identifies the <see cref="PanePlacement"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PanePlacementProperty =
            DependencyProperty.Register(
                nameof(PanePlacement),
                typeof(SplitViewPanePlacement),
                typeof(SplitView),
                new PropertyMetadata(
                    SplitViewPanePlacement.Left,
                    DisplayModeProperty_Changed));

        /// <summary>
        /// Identifies the <see cref="DisplayMode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayModeProperty =
            DependencyProperty.Register(
                nameof(DisplayMode),
                typeof(SplitViewDisplayMode),
                typeof(SplitView),
                new PropertyMetadata(
                    SplitViewDisplayMode.Overlay,
                    DisplayModeProperty_Changed));

        /// <summary>
        /// Identifies the <see cref="IsPaneOpen"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaneOpenProperty =
            DependencyProperty.Register(
                nameof(IsPaneOpen),
                typeof(bool),
                typeof(SplitView),
                new PropertyMetadata(
                    true,
                    IsPaneOpen_Changed));

        /// <summary>
        /// Identifies the <see cref="CompactPaneLength"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CompactPaneLengthProperty =
            DependencyProperty.Register(
                nameof(CompactPaneLength),
                typeof(double),
                typeof(SplitView),
                new PropertyMetadata(48d));

        /// <summary>
        /// Identifies the <see cref="OpenPaneLength"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OpenPaneLengthProperty =
            DependencyProperty.Register(
                nameof(OpenPaneLength),
                typeof(double),
                typeof(SplitView),
                new PropertyMetadata(320d));

        /// <summary>
        /// Identifies the <see cref="PaneBackground"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneBackgroundProperty =
            DependencyProperty.Register(
                nameof(PaneBackground),
                typeof(Brush),
                typeof(SplitView),
                new PropertyMetadata(Brushes.Transparent));

        private static readonly DependencyPropertyKey HasPanePropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasPane),
                typeof(bool),
                typeof(SplitView),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="HasPane"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasPaneProperty =
            HasPanePropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="Pane"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneProperty =
            DependencyProperty.Register(
                nameof(Pane),
                typeof(object),
                typeof(SplitView),
                new PropertyMetadata(
                    null,
                    Pane_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneTemplateProperty =
            DependencyProperty.Register(
                nameof(PaneTemplate),
                typeof(DataTemplate),
                typeof(SplitView),
                new PropertyMetadata(
                    null,
                    PaneTemplate_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(PaneTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(SplitView),
                new PropertyMetadata(
                    null,
                    PaneTemplateSelector_Changed));

        /// <summary>
        /// Identifies the <see cref="PaneStringFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneStringFormatProperty =
            DependencyProperty.Register(
                nameof(PaneStringFormat),
                typeof(string),
                typeof(SplitView),
                new PropertyMetadata(
                    null,
                    PaneStringFormat_Changed));

        /// <summary>
        /// Gets or sets the position at which the pane is placed inside the
        /// <see cref="SplitView"/>.
        /// </summary>
        public SplitViewPanePlacement PanePlacement
        {
            get { return (SplitViewPanePlacement)GetValue(PanePlacementProperty); }
            set { SetValue(PanePlacementProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current display mode of the <see cref="SplitView"/>,
        /// defining how the pane and content are layed out.
        /// </summary>
        public SplitViewDisplayMode DisplayMode
        {
            get { return (SplitViewDisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the pane is currently opened.
        /// </summary>
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }

        /// <summary>
        /// Gets or sets the length of the pane when it is closed and when it's 
        /// <see cref="DisplayMode"/> is set to <see cref="SplitViewDisplayMode.CompactOverlay"/>
        /// or <see cref="SplitViewDisplayMode.CompactInline"/>.
        /// </summary>
        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the length of the pane when it is opened.
        /// </summary>
        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background brush of the pane.
        /// </summary>
        public Brush PaneBackground
        {
            get { return (Brush)GetValue(PaneBackgroundProperty); }
            set { SetValue(PaneBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Pane"/> property of this
        /// <see cref="SplitView"/> holds any content.
        /// </summary>
        [Bindable(false), Browsable(false)]
        public bool HasPane
        {
            get { return (bool)GetValue(HasPaneProperty); }
            private set { SetValue(HasPanePropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the content which is being displayed in the pane of the
        /// <see cref="SplitView"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        [Localizability(LocalizationCategory.Label)]
        public object Pane
        {
            get { return (object)GetValue(PaneProperty); }
            set { SetValue(PaneProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
        /// <see cref="Pane"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplate PaneTemplate
        {
            get { return (DataTemplate)GetValue(PaneTemplateProperty); }
            set { SetValue(PaneTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
        /// to provide custom template-selection logic for the <see cref="Pane"/> content.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplateSelector PaneTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(PaneTemplateSelectorProperty); }
            set { SetValue(PaneTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="Pane"/>
        /// content property, if it is displayed as a string.
        /// </summary>
        [Bindable(true), Category("Content")]
        public string PaneStringFormat
        {
            get { return (string)GetValue(PaneStringFormatProperty); }
            set { SetValue(PaneStringFormatProperty, value); }
        }

        private static void Pane_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (SplitView)d;
            self.HasPane = e.NewValue != null;
            self.OnPaneChanged(e.OldValue, e.NewValue);
        }

        private static void PaneTemplate_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (SplitView)d;
            self.OnPaneTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        private static void PaneTemplateSelector_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (SplitView)d;
            self.OnPaneTemplateSelectorChanged(
                (DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
        }

        private static void PaneStringFormat_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (SplitView)d;
            self.OnPaneStringFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        /// <summary>
        /// Called when the <see cref="Pane"/> property was changed.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="Pane"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="Pane"/> property.</param>
        protected virtual void OnPaneChanged(object oldValue, object newValue)
        {
            RemoveLogicalChild(oldValue);
            AddLogicalChild(newValue);
        }

        /// <summary>
        /// Called when the <see cref="PaneTemplate"/> property was changed.
        /// </summary>
        /// <param name="oldTemplate">The old value of the <see cref="PaneTemplate"/> property.</param>
        /// <param name="newTemplate">The new value of the <see cref="PaneTemplate"/> property.</param>
        protected virtual void OnPaneTemplateChanged(
            DataTemplate oldTemplate, DataTemplate newTemplate)
        { }

        /// <summary>
        /// Called when the <see cref="PaneTemplateSelector"/> property was changed.
        /// </summary>
        /// <param name="oldSelector">
        /// The old value of the <see cref="PaneTemplateSelector"/> property.
        /// </param>
        /// <param name="newSelector">
        /// The new value of the <see cref="PaneTemplateSelector"/> property.
        /// </param>
        protected virtual void OnPaneTemplateSelectorChanged(
            DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
        { }

        /// <summary>
        /// Called when the <see cref="PaneStringFormat"/> property was changed.
        /// </summary>
        /// <param name="oldStringFormat">
        /// The old value of the <see cref="PaneStringFormat"/> property.
        /// </param>
        /// <param name="newStringFormat">
        /// The new value of the <see cref="PaneStringFormat"/> property.
        /// </param>
        protected virtual void OnPaneStringFormatChanged(
            string oldStringFormat, string newStringFormat)
        { }
        
    }

}
