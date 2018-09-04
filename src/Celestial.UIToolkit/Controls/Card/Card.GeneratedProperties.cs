using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections;

// This file is auto-generated.
// Any changes made to this file will be lost.

namespace Celestial.UIToolkit.Controls
{

    public partial class Card : ContentControl
    {
	
	/// <summary>
	/// Gets an enumerator on the <see cref="Card"/>'s logical children.
	/// </summary>
	protected override IEnumerator LogicalChildren
    {
        get
        {
			yield return base.LogicalChildren;
			
						if (Title != null)
			    yield return Title;
						if (SubTitle != null)
			    yield return SubTitle;
						if (Thumbnail != null)
			    yield return Thumbnail;
						if (MediaContent != null)
			    yield return MediaContent;
					}
	}


    	
	private static readonly DependencyPropertyKey HasTitlePropertyKey =
		DependencyProperty.RegisterReadOnly(
			nameof(HasTitle),
			typeof(bool),
			typeof(Card),
			new PropertyMetadata(false));
	/// <summary>
	/// Identifies the <see cref="HasTitle"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty HasTitleProperty =
		HasTitlePropertyKey.DependencyProperty;

	/// <summary>
	/// Identifies the <see cref="Title"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty TitleProperty =
		DependencyProperty.Register(
			nameof(Title),
			typeof(object),
			typeof(Card),
			new PropertyMetadata(
				null,
				Title_Changed));

	/// <summary>
	/// Identifies the <see cref="TitleTemplate"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty TitleTemplateProperty =
		DependencyProperty.Register(
			nameof(TitleTemplate),
			typeof(DataTemplate),
			typeof(Card),
			new PropertyMetadata(
				null,
				TitleTemplate_Changed));

	/// <summary>
	/// Identifies the <see cref="TitleTemplateSelector"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty TitleTemplateSelectorProperty =
		DependencyProperty.Register(
			nameof(TitleTemplateSelector),
			typeof(DataTemplateSelector),
			typeof(Card),
			new PropertyMetadata(
				null,
				TitleTemplateSelector_Changed));

	/// <summary>
	/// Identifies the <see cref="TitleStringFormat"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty TitleStringFormatProperty =
		DependencyProperty.Register(
			nameof(TitleStringFormat),
			typeof(string),
			typeof(Card),
			new PropertyMetadata(
				null,
				TitleStringFormat_Changed));
            
	/// <summary>
	/// Gets a value indicating whether the <see cref="Title"/> property of this
	/// <see cref="Card"/> holds any content.
	/// </summary>
	[Bindable(false), Browsable(false)]
	public bool HasTitle
	{
		get { return (bool)GetValue(HasTitleProperty); }
		private set { SetValue(HasTitlePropertyKey, value); }
	}

	/// <summary>
	/// Gets or sets the content which is being displayed in the Title of the
	/// <see cref="Card"/>.
	/// </summary>
	[Bindable(true), Category("Content")]
	[Localizability(LocalizationCategory.Label)]
	public object Title
	{
		get { return (object)GetValue(TitleProperty); }
		set { SetValue(TitleProperty, value); }
	}

	/// <summary>
	/// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
	/// <see cref="Title"/> content.
	/// </summary>
	[Bindable(true), Category("Content")]
	public DataTemplate TitleTemplate
	{
		get { return (DataTemplate)GetValue(TitleTemplateProperty); }
		set { SetValue(TitleTemplateProperty, value); }
	}

	/// <summary>
	/// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
	/// to provide custom template-selection logic for the <see cref="Title"/> content.
	/// </summary>
	[Bindable(true), Category("Content")]
	public DataTemplateSelector TitleTemplateSelector
	{
		get { return (DataTemplateSelector)GetValue(TitleTemplateSelectorProperty); }
		set { SetValue(TitleTemplateSelectorProperty, value); }
	}

	/// <summary>
	/// Gets or sets a composite string that specifies how to format the <see cref="Title"/>
	/// content property, if it is displayed as a string.
	/// </summary>
	[Bindable(true), Category("Content")]
	public string TitleStringFormat
	{
		get { return (string)GetValue(TitleStringFormatProperty); }
		set { SetValue(TitleStringFormatProperty, value); }
	}

	private static void Title_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.HasTitle = e.NewValue != null;
		self.OnTitleChanged(e.OldValue, e.NewValue);
	}

	private static void TitleTemplate_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnTitleTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
	}

	private static void TitleTemplateSelector_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnTitleTemplateSelectorChanged(
			(DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
	}

	private static void TitleStringFormat_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnTitleStringFormatChanged((string)e.OldValue, (string)e.NewValue);
	}

	/// <summary>
	/// Called when the <see cref="Title"/> property was changed.
	/// </summary>
	/// <param name="oldValue">The old value of the <see cref="Title"/> property.</param>
	/// <param name="newValue">The new value of the <see cref="Title"/> property.</param>
	protected virtual void OnTitleChanged(object oldValue, object newValue)
	{
		RemoveLogicalChild(oldValue);
		AddLogicalChild(newValue);
	}

	/// <summary>
	/// Called when the <see cref="TitleTemplate"/> property was changed.
	/// </summary>
	/// <param name="oldTemplate">The old value of the <see cref="TitleTemplate"/> property.</param>
	/// <param name="newTemplate">The new value of the <see cref="TitleTemplate"/> property.</param>
	protected virtual void OnTitleTemplateChanged(
		DataTemplate oldTemplate, DataTemplate newTemplate)
	{ }

	/// <summary>
	/// Called when the <see cref="TitleTemplateSelector"/> property was changed.
	/// </summary>
	/// <param name="oldSelector">
	/// The old value of the <see cref="TitleTemplateSelector"/> property.
	/// </param>
	/// <param name="newSelector">
	/// The new value of the <see cref="TitleTemplateSelector"/> property.
	/// </param>
	protected virtual void OnTitleTemplateSelectorChanged(
		DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
	{ }

	/// <summary>
	/// Called when the <see cref="TitleStringFormat"/> property was changed.
	/// </summary>
	/// <param name="oldStringFormat">
	/// The old value of the <see cref="TitleStringFormat"/> property.
	/// </param>
	/// <param name="newStringFormat">
	/// The new value of the <see cref="TitlePropertyStringFormat"/> property.
	/// </param>
	protected virtual void OnTitleStringFormatChanged(
		string oldStringFormat, string newStringFormat)
	{ }
    
    	
	private static readonly DependencyPropertyKey HasSubTitlePropertyKey =
		DependencyProperty.RegisterReadOnly(
			nameof(HasSubTitle),
			typeof(bool),
			typeof(Card),
			new PropertyMetadata(false));
	/// <summary>
	/// Identifies the <see cref="HasSubTitle"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty HasSubTitleProperty =
		HasSubTitlePropertyKey.DependencyProperty;

	/// <summary>
	/// Identifies the <see cref="SubTitle"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty SubTitleProperty =
		DependencyProperty.Register(
			nameof(SubTitle),
			typeof(object),
			typeof(Card),
			new PropertyMetadata(
				null,
				SubTitle_Changed));

	/// <summary>
	/// Identifies the <see cref="SubTitleTemplate"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty SubTitleTemplateProperty =
		DependencyProperty.Register(
			nameof(SubTitleTemplate),
			typeof(DataTemplate),
			typeof(Card),
			new PropertyMetadata(
				null,
				SubTitleTemplate_Changed));

	/// <summary>
	/// Identifies the <see cref="SubTitleTemplateSelector"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty SubTitleTemplateSelectorProperty =
		DependencyProperty.Register(
			nameof(SubTitleTemplateSelector),
			typeof(DataTemplateSelector),
			typeof(Card),
			new PropertyMetadata(
				null,
				SubTitleTemplateSelector_Changed));

	/// <summary>
	/// Identifies the <see cref="SubTitleStringFormat"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty SubTitleStringFormatProperty =
		DependencyProperty.Register(
			nameof(SubTitleStringFormat),
			typeof(string),
			typeof(Card),
			new PropertyMetadata(
				null,
				SubTitleStringFormat_Changed));
            
	/// <summary>
	/// Gets a value indicating whether the <see cref="SubTitle"/> property of this
	/// <see cref="Card"/> holds any content.
	/// </summary>
	[Bindable(false), Browsable(false)]
	public bool HasSubTitle
	{
		get { return (bool)GetValue(HasSubTitleProperty); }
		private set { SetValue(HasSubTitlePropertyKey, value); }
	}

	/// <summary>
	/// Gets or sets the content which is being displayed in the SubTitle of the
	/// <see cref="Card"/>.
	/// </summary>
	[Bindable(true), Category("Content")]
	[Localizability(LocalizationCategory.Label)]
	public object SubTitle
	{
		get { return (object)GetValue(SubTitleProperty); }
		set { SetValue(SubTitleProperty, value); }
	}

	/// <summary>
	/// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
	/// <see cref="SubTitle"/> content.
	/// </summary>
	[Bindable(true), Category("Content")]
	public DataTemplate SubTitleTemplate
	{
		get { return (DataTemplate)GetValue(SubTitleTemplateProperty); }
		set { SetValue(SubTitleTemplateProperty, value); }
	}

	/// <summary>
	/// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
	/// to provide custom template-selection logic for the <see cref="SubTitle"/> content.
	/// </summary>
	[Bindable(true), Category("Content")]
	public DataTemplateSelector SubTitleTemplateSelector
	{
		get { return (DataTemplateSelector)GetValue(SubTitleTemplateSelectorProperty); }
		set { SetValue(SubTitleTemplateSelectorProperty, value); }
	}

	/// <summary>
	/// Gets or sets a composite string that specifies how to format the <see cref="SubTitle"/>
	/// content property, if it is displayed as a string.
	/// </summary>
	[Bindable(true), Category("Content")]
	public string SubTitleStringFormat
	{
		get { return (string)GetValue(SubTitleStringFormatProperty); }
		set { SetValue(SubTitleStringFormatProperty, value); }
	}

	private static void SubTitle_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.HasSubTitle = e.NewValue != null;
		self.OnSubTitleChanged(e.OldValue, e.NewValue);
	}

	private static void SubTitleTemplate_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnSubTitleTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
	}

	private static void SubTitleTemplateSelector_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnSubTitleTemplateSelectorChanged(
			(DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
	}

	private static void SubTitleStringFormat_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnSubTitleStringFormatChanged((string)e.OldValue, (string)e.NewValue);
	}

	/// <summary>
	/// Called when the <see cref="SubTitle"/> property was changed.
	/// </summary>
	/// <param name="oldValue">The old value of the <see cref="SubTitle"/> property.</param>
	/// <param name="newValue">The new value of the <see cref="SubTitle"/> property.</param>
	protected virtual void OnSubTitleChanged(object oldValue, object newValue)
	{
		RemoveLogicalChild(oldValue);
		AddLogicalChild(newValue);
	}

	/// <summary>
	/// Called when the <see cref="SubTitleTemplate"/> property was changed.
	/// </summary>
	/// <param name="oldTemplate">The old value of the <see cref="SubTitleTemplate"/> property.</param>
	/// <param name="newTemplate">The new value of the <see cref="SubTitleTemplate"/> property.</param>
	protected virtual void OnSubTitleTemplateChanged(
		DataTemplate oldTemplate, DataTemplate newTemplate)
	{ }

	/// <summary>
	/// Called when the <see cref="SubTitleTemplateSelector"/> property was changed.
	/// </summary>
	/// <param name="oldSelector">
	/// The old value of the <see cref="SubTitleTemplateSelector"/> property.
	/// </param>
	/// <param name="newSelector">
	/// The new value of the <see cref="SubTitleTemplateSelector"/> property.
	/// </param>
	protected virtual void OnSubTitleTemplateSelectorChanged(
		DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
	{ }

	/// <summary>
	/// Called when the <see cref="SubTitleStringFormat"/> property was changed.
	/// </summary>
	/// <param name="oldStringFormat">
	/// The old value of the <see cref="SubTitleStringFormat"/> property.
	/// </param>
	/// <param name="newStringFormat">
	/// The new value of the <see cref="SubTitlePropertyStringFormat"/> property.
	/// </param>
	protected virtual void OnSubTitleStringFormatChanged(
		string oldStringFormat, string newStringFormat)
	{ }
    
    	
	private static readonly DependencyPropertyKey HasThumbnailPropertyKey =
		DependencyProperty.RegisterReadOnly(
			nameof(HasThumbnail),
			typeof(bool),
			typeof(Card),
			new PropertyMetadata(false));
	/// <summary>
	/// Identifies the <see cref="HasThumbnail"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty HasThumbnailProperty =
		HasThumbnailPropertyKey.DependencyProperty;

	/// <summary>
	/// Identifies the <see cref="Thumbnail"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty ThumbnailProperty =
		DependencyProperty.Register(
			nameof(Thumbnail),
			typeof(object),
			typeof(Card),
			new PropertyMetadata(
				null,
				Thumbnail_Changed));

	/// <summary>
	/// Identifies the <see cref="ThumbnailTemplate"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty ThumbnailTemplateProperty =
		DependencyProperty.Register(
			nameof(ThumbnailTemplate),
			typeof(DataTemplate),
			typeof(Card),
			new PropertyMetadata(
				null,
				ThumbnailTemplate_Changed));

	/// <summary>
	/// Identifies the <see cref="ThumbnailTemplateSelector"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty ThumbnailTemplateSelectorProperty =
		DependencyProperty.Register(
			nameof(ThumbnailTemplateSelector),
			typeof(DataTemplateSelector),
			typeof(Card),
			new PropertyMetadata(
				null,
				ThumbnailTemplateSelector_Changed));

	/// <summary>
	/// Identifies the <see cref="ThumbnailStringFormat"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty ThumbnailStringFormatProperty =
		DependencyProperty.Register(
			nameof(ThumbnailStringFormat),
			typeof(string),
			typeof(Card),
			new PropertyMetadata(
				null,
				ThumbnailStringFormat_Changed));
            
	/// <summary>
	/// Gets a value indicating whether the <see cref="Thumbnail"/> property of this
	/// <see cref="Card"/> holds any content.
	/// </summary>
	[Bindable(false), Browsable(false)]
	public bool HasThumbnail
	{
		get { return (bool)GetValue(HasThumbnailProperty); }
		private set { SetValue(HasThumbnailPropertyKey, value); }
	}

	/// <summary>
	/// Gets or sets the content which is being displayed in the Thumbnail of the
	/// <see cref="Card"/>.
	/// </summary>
	[Bindable(true), Category("Content")]
	[Localizability(LocalizationCategory.Label)]
	public object Thumbnail
	{
		get { return (object)GetValue(ThumbnailProperty); }
		set { SetValue(ThumbnailProperty, value); }
	}

	/// <summary>
	/// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
	/// <see cref="Thumbnail"/> content.
	/// </summary>
	[Bindable(true), Category("Content")]
	public DataTemplate ThumbnailTemplate
	{
		get { return (DataTemplate)GetValue(ThumbnailTemplateProperty); }
		set { SetValue(ThumbnailTemplateProperty, value); }
	}

	/// <summary>
	/// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
	/// to provide custom template-selection logic for the <see cref="Thumbnail"/> content.
	/// </summary>
	[Bindable(true), Category("Content")]
	public DataTemplateSelector ThumbnailTemplateSelector
	{
		get { return (DataTemplateSelector)GetValue(ThumbnailTemplateSelectorProperty); }
		set { SetValue(ThumbnailTemplateSelectorProperty, value); }
	}

	/// <summary>
	/// Gets or sets a composite string that specifies how to format the <see cref="Thumbnail"/>
	/// content property, if it is displayed as a string.
	/// </summary>
	[Bindable(true), Category("Content")]
	public string ThumbnailStringFormat
	{
		get { return (string)GetValue(ThumbnailStringFormatProperty); }
		set { SetValue(ThumbnailStringFormatProperty, value); }
	}

	private static void Thumbnail_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.HasThumbnail = e.NewValue != null;
		self.OnThumbnailChanged(e.OldValue, e.NewValue);
	}

	private static void ThumbnailTemplate_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnThumbnailTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
	}

	private static void ThumbnailTemplateSelector_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnThumbnailTemplateSelectorChanged(
			(DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
	}

	private static void ThumbnailStringFormat_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnThumbnailStringFormatChanged((string)e.OldValue, (string)e.NewValue);
	}

	/// <summary>
	/// Called when the <see cref="Thumbnail"/> property was changed.
	/// </summary>
	/// <param name="oldValue">The old value of the <see cref="Thumbnail"/> property.</param>
	/// <param name="newValue">The new value of the <see cref="Thumbnail"/> property.</param>
	protected virtual void OnThumbnailChanged(object oldValue, object newValue)
	{
		RemoveLogicalChild(oldValue);
		AddLogicalChild(newValue);
	}

	/// <summary>
	/// Called when the <see cref="ThumbnailTemplate"/> property was changed.
	/// </summary>
	/// <param name="oldTemplate">The old value of the <see cref="ThumbnailTemplate"/> property.</param>
	/// <param name="newTemplate">The new value of the <see cref="ThumbnailTemplate"/> property.</param>
	protected virtual void OnThumbnailTemplateChanged(
		DataTemplate oldTemplate, DataTemplate newTemplate)
	{ }

	/// <summary>
	/// Called when the <see cref="ThumbnailTemplateSelector"/> property was changed.
	/// </summary>
	/// <param name="oldSelector">
	/// The old value of the <see cref="ThumbnailTemplateSelector"/> property.
	/// </param>
	/// <param name="newSelector">
	/// The new value of the <see cref="ThumbnailTemplateSelector"/> property.
	/// </param>
	protected virtual void OnThumbnailTemplateSelectorChanged(
		DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
	{ }

	/// <summary>
	/// Called when the <see cref="ThumbnailStringFormat"/> property was changed.
	/// </summary>
	/// <param name="oldStringFormat">
	/// The old value of the <see cref="ThumbnailStringFormat"/> property.
	/// </param>
	/// <param name="newStringFormat">
	/// The new value of the <see cref="ThumbnailPropertyStringFormat"/> property.
	/// </param>
	protected virtual void OnThumbnailStringFormatChanged(
		string oldStringFormat, string newStringFormat)
	{ }
    
    	
	private static readonly DependencyPropertyKey HasMediaContentPropertyKey =
		DependencyProperty.RegisterReadOnly(
			nameof(HasMediaContent),
			typeof(bool),
			typeof(Card),
			new PropertyMetadata(false));
	/// <summary>
	/// Identifies the <see cref="HasMediaContent"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty HasMediaContentProperty =
		HasMediaContentPropertyKey.DependencyProperty;

	/// <summary>
	/// Identifies the <see cref="MediaContent"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty MediaContentProperty =
		DependencyProperty.Register(
			nameof(MediaContent),
			typeof(object),
			typeof(Card),
			new PropertyMetadata(
				null,
				MediaContent_Changed));

	/// <summary>
	/// Identifies the <see cref="MediaContentTemplate"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty MediaContentTemplateProperty =
		DependencyProperty.Register(
			nameof(MediaContentTemplate),
			typeof(DataTemplate),
			typeof(Card),
			new PropertyMetadata(
				null,
				MediaContentTemplate_Changed));

	/// <summary>
	/// Identifies the <see cref="MediaContentTemplateSelector"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty MediaContentTemplateSelectorProperty =
		DependencyProperty.Register(
			nameof(MediaContentTemplateSelector),
			typeof(DataTemplateSelector),
			typeof(Card),
			new PropertyMetadata(
				null,
				MediaContentTemplateSelector_Changed));

	/// <summary>
	/// Identifies the <see cref="MediaContentStringFormat"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty MediaContentStringFormatProperty =
		DependencyProperty.Register(
			nameof(MediaContentStringFormat),
			typeof(string),
			typeof(Card),
			new PropertyMetadata(
				null,
				MediaContentStringFormat_Changed));
            
	/// <summary>
	/// Gets a value indicating whether the <see cref="MediaContent"/> property of this
	/// <see cref="Card"/> holds any content.
	/// </summary>
	[Bindable(false), Browsable(false)]
	public bool HasMediaContent
	{
		get { return (bool)GetValue(HasMediaContentProperty); }
		private set { SetValue(HasMediaContentPropertyKey, value); }
	}

	/// <summary>
	/// Gets or sets the content which is being displayed in the MediaContent of the
	/// <see cref="Card"/>.
	/// </summary>
	[Bindable(true), Category("Content")]
	[Localizability(LocalizationCategory.Label)]
	public object MediaContent
	{
		get { return (object)GetValue(MediaContentProperty); }
		set { SetValue(MediaContentProperty, value); }
	}

	/// <summary>
	/// Gets or sets a <see cref="DataTemplate"/> to be used for displaying the 
	/// <see cref="MediaContent"/> content.
	/// </summary>
	[Bindable(true), Category("Content")]
	public DataTemplate MediaContentTemplate
	{
		get { return (DataTemplate)GetValue(MediaContentTemplateProperty); }
		set { SetValue(MediaContentTemplateProperty, value); }
	}

	/// <summary>
	/// Gets or sets <see cref="DataTemplateSelector"/> which enables an application writer
	/// to provide custom template-selection logic for the <see cref="MediaContent"/> content.
	/// </summary>
	[Bindable(true), Category("Content")]
	public DataTemplateSelector MediaContentTemplateSelector
	{
		get { return (DataTemplateSelector)GetValue(MediaContentTemplateSelectorProperty); }
		set { SetValue(MediaContentTemplateSelectorProperty, value); }
	}

	/// <summary>
	/// Gets or sets a composite string that specifies how to format the <see cref="MediaContent"/>
	/// content property, if it is displayed as a string.
	/// </summary>
	[Bindable(true), Category("Content")]
	public string MediaContentStringFormat
	{
		get { return (string)GetValue(MediaContentStringFormatProperty); }
		set { SetValue(MediaContentStringFormatProperty, value); }
	}

	private static void MediaContent_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.HasMediaContent = e.NewValue != null;
		self.OnMediaContentChanged(e.OldValue, e.NewValue);
	}

	private static void MediaContentTemplate_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnMediaContentTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
	}

	private static void MediaContentTemplateSelector_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnMediaContentTemplateSelectorChanged(
			(DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
	}

	private static void MediaContentStringFormat_Changed(
		DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var self = (Card)d;
		self.OnMediaContentStringFormatChanged((string)e.OldValue, (string)e.NewValue);
	}

	/// <summary>
	/// Called when the <see cref="MediaContent"/> property was changed.
	/// </summary>
	/// <param name="oldValue">The old value of the <see cref="MediaContent"/> property.</param>
	/// <param name="newValue">The new value of the <see cref="MediaContent"/> property.</param>
	protected virtual void OnMediaContentChanged(object oldValue, object newValue)
	{
		RemoveLogicalChild(oldValue);
		AddLogicalChild(newValue);
	}

	/// <summary>
	/// Called when the <see cref="MediaContentTemplate"/> property was changed.
	/// </summary>
	/// <param name="oldTemplate">The old value of the <see cref="MediaContentTemplate"/> property.</param>
	/// <param name="newTemplate">The new value of the <see cref="MediaContentTemplate"/> property.</param>
	protected virtual void OnMediaContentTemplateChanged(
		DataTemplate oldTemplate, DataTemplate newTemplate)
	{ }

	/// <summary>
	/// Called when the <see cref="MediaContentTemplateSelector"/> property was changed.
	/// </summary>
	/// <param name="oldSelector">
	/// The old value of the <see cref="MediaContentTemplateSelector"/> property.
	/// </param>
	/// <param name="newSelector">
	/// The new value of the <see cref="MediaContentTemplateSelector"/> property.
	/// </param>
	protected virtual void OnMediaContentTemplateSelectorChanged(
		DataTemplateSelector oldSelector, DataTemplateSelector newSelector)
	{ }

	/// <summary>
	/// Called when the <see cref="MediaContentStringFormat"/> property was changed.
	/// </summary>
	/// <param name="oldStringFormat">
	/// The old value of the <see cref="MediaContentStringFormat"/> property.
	/// </param>
	/// <param name="newStringFormat">
	/// The new value of the <see cref="MediaContentPropertyStringFormat"/> property.
	/// </param>
	protected virtual void OnMediaContentStringFormatChanged(
		string oldStringFormat, string newStringFormat)
	{ }
    
        }

}

