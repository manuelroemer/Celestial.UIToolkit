using Celestial.UIToolkit.Extensions;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Celestial.UIToolkit.Controls
{

    // This whole class is guided by:
    // https://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Controls/ContentControl.cs,0a1ab68e41442eeb
    // The goal is to mimick the real ContentControl, but only allow UIElements as children.
    

    /// <summary>
    /// A control similar to the <see cref="ContentControl"/>, with the difference that this one
    /// only accepts <see cref="UIElement"/> objects as children and directly adds them to the
    /// logical tree, without using any placeholder mechanism.
    /// </summary>
    [DefaultProperty(nameof(Content))]
    [ContentProperty(nameof(Content))]
    public class UIElementContentControl : Control, IAddChild
    {
        
        /// <summary>
        /// Identifies the <see cref="Content"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                nameof(Content),
                typeof(UIElement),
                typeof(UIElementContentControl),
                new PropertyMetadata(
                    null,
                    Content_Changed));

        private static readonly DependencyPropertyKey HasContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(HasContent),
                typeof(bool),
                typeof(UIElementContentControl),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.None));

        /// <summary>
        /// Identifies the <see cref="HasContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HasContentProperty =
            HasContentPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets the single child of the <see cref="UIElementContentControl"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        public UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Content"/> property holds
        /// any value.
        /// </summary>
        [Bindable(true), Browsable(false), ReadOnly(true)]
        public bool HasContent
        {
            get { return (bool)GetValue(HasContentProperty); }
            private set { SetValue(HasContentPropertyKey, value); }
        }

        /// <summary>
        /// Gets an enumerator on the logical children of this
        /// <see cref="UIElementContentControl"/>.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (Content == null)
                    yield break;

                // The Content can have a TemplatedParent as a logical parent.
                // In this case, this UIElementContentControl is not the parent of the content.
                if (IsContentLogicalChildOfTemplatedParent)
                    yield break;

                // All good, Content is not null and we are the parent.
                yield return Content;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Content"/> is the logical child
        /// of the control's logical parent.
        /// This can happen, if this control is placed inside a control template and is also
        /// displying the template's content.
        /// </summary>
        protected bool IsContentLogicalChildOfTemplatedParent
        {
            get
            {
                if (TemplatedParent != null && Content != null)
                {
                    DependencyObject contentLogicalParent = LogicalTreeHelper.GetParent(Content);
                    return contentLogicalParent != null && contentLogicalParent != this;
                }
                return false;
            }
        }

        static UIElementContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(UIElementContentControl),
                new FrameworkPropertyMetadata(typeof(UIElementContentControl)));
        }

        void IAddChild.AddChild(object value)
        {
            AddChild((UIElement)value);
        }

        void IAddChild.AddText(string text)
        {
            AddText(text);
        }

        /// <summary>
        /// Adds the specified <see cref="UIElement"/> as a child of the
        /// <see cref="UIElementContentControl"/>.
        /// </summary>
        /// <param name="child">
        /// The child to be added. Can be null for clearing the <see cref="Content"/>.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="UIElementContentControl"/> already has a child.
        /// </exception>
        protected virtual void AddChild(UIElement child)
        {
            if (Content == null || child == null)
            {
                Content = child;
            }
            else
            {
                throw new InvalidOperationException(
                    $"The {nameof(UIElementContentControl)} can only have a single child.");
            }
        }

        /// <summary>
        /// Wraps the specified <paramref name="text"/> into a <see cref="TextBlock"/> and then
        /// adds that <see cref="TextBlock"/> as the control's child.
        /// </summary>
        /// <param name="text">The text to be added as a visual child.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="UIElementContentControl"/> already has a child.
        /// </exception>
        protected virtual void AddText(string text)
        {
            if (text != null)
            {
                // Wrap any type of text in a TextBlock.
                var textBlockWrapper = new TextBlock(new Run(text));
                AddChild(textBlockWrapper);
            }
            else
            {
                // null text ^= Clearing the Content
                AddChild(null);
            }
        }

        /// <summary>
        /// Returns a value indicating whether the <see cref="Content"/> property should
        /// be serialized.
        /// </summary>
        /// <returns>
        /// A value indicating whether <see cref="Content"/> should be serialized.
        /// </returns>
        public virtual bool ShouldSerializeContent()
        {
            return ReadLocalValue(ContentProperty) != DependencyProperty.UnsetValue;
        }

        private static void Content_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (UIElementContentControl)d;
            var oldValue = (UIElement)e.OldValue;
            var newValue = (UIElement)e.NewValue;

            self.HasContent = newValue != null;
            self.OnContentChanged(oldValue, newValue);
        }

        /// <summary>
        /// Called when the <see cref="Content"/> property gets changed.
        /// </summary>
        /// <param name="oldContent">The old content.</param>
        /// <param name="newContent">The new content.</param>
        protected virtual void OnContentChanged(UIElement oldContent, UIElement newContent)
        {
            RemoveLogicalChild(oldContent);
            
            if (!IsContentLogicalChildOfTemplatedParent)
            {
                LogicalTreeHelper.GetParent(newContent)?.RemoveLogicalChild(newContent);
                AddLogicalChild(newContent);
            }
        }
        
        /// <summary>
        /// Returns a string representation of this control.
        /// </summary>
        /// <returns>A string representing the control.</returns>
        public override string ToString()
        {
            return $"{nameof(UIElementContentControl)}: " +
                   $"{nameof(Content)}: {Content}";
        }

    }

}
