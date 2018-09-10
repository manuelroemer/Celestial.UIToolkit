using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Theming
{

    // This file provides helper properties for the TextBox control template.
    // To be more precise, it provides an attached 'HasText' property for the
    // - TextBox
    // - RichTextBox
    // - PasswordBox
    // classes. All of these require different ways of determining whether they contain any text.
    // In addition, the RTB/PasswordBox doesn't provide a way to bind to the actual text.
    // => We need to rely on attaching events.
    // 
    // This class relies on a little "hack" though, meaning that the event handlers are hooked
    // through an attached property.
    // Hence, this class is kept internal, because I don't want the common user to interfere with
    // this process.

    internal static class TextBoxHelper
    {
        
        public static readonly DependencyProperty TextBoxProperty =
            DependencyProperty.RegisterAttached(
                "TextBox",
                typeof(FrameworkElement),
                typeof(TextBoxHelper),
                new PropertyMetadata(
                    null,
                    TextBox_Changed));

        public static FrameworkElement GetTextBox(DependencyObject obj) =>
            (FrameworkElement)obj.GetValue(TextBoxProperty);
        
        public static void SetTextBox(DependencyObject obj, FrameworkElement value) =>
            obj.SetValue(TextBoxProperty, value);


        
        public static readonly DependencyProperty HasTextProperty =
            DependencyProperty.RegisterAttached(
                "HasText",
                typeof(bool),
                typeof(TextBoxHelper),
                new PropertyMetadata(false));
        
        public static bool GetHasText(DependencyObject obj) =>
            (bool)obj.GetValue(HasTextProperty);
        
        public static void SetHasText(DependencyObject obj, bool value) =>
            obj.SetValue(HasTextProperty, value);



        private static void TextBox_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // When this property gets set, attach the correct event handler,
            // so that we can listen to text changes.
            if (d is TextBox textBox)
            {
                WeakEventManager<TextBox, TextChangedEventArgs>.AddHandler(
                    textBox,
                    nameof(TextBox.TextChanged),
                    TextBox_TextChanged);
                UpdateHasTextForTextBox(textBox);
                textBox.TraceVerbose("TextBoxHelper - Attached TextChanged handler.");
            }
            else if (d is RichTextBox richTextBox)
            {
                WeakEventManager<RichTextBox, TextChangedEventArgs>.AddHandler(
                    richTextBox,
                    nameof(RichTextBox.TextChanged),
                    RichTextBox_TextChanged);
                UpdateHasTextForRichTextBox(richTextBox);
                richTextBox.TraceVerbose("TextBoxHelper - Attached TextChanged handler.");
            }
            else if (d is PasswordBox passwordBox)
            {
                WeakEventManager<PasswordBox, RoutedEventArgs>.AddHandler(
                    passwordBox,
                    nameof(PasswordBox.PasswordChanged),
                    PasswordBox_PasswordChanged);
                UpdateHasTextForPasswordBox(passwordBox);
                passwordBox.TraceVerbose("TextBoxHelper - Attached PasswordChanged handler.");
            }

            // Clear the value to not create memory leaks.
            // If this isn't called, the dep.prop. stores a reference to itself,
            // forever and ever.
            d.ClearValue(TextBoxProperty);
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateHasTextForTextBox((TextBox)sender);
        }

        private static void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateHasTextForRichTextBox((RichTextBox)sender);
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateHasTextForPasswordBox((PasswordBox)sender);
        }

        private static void UpdateHasTextForTextBox(TextBox textBox)
        {
            bool hasText = !string.IsNullOrEmpty(textBox.Text);
            SetHasText(textBox, hasText);
            textBox.TraceVerbose("HasText set to {0}.", hasText);
        }

        private static void UpdateHasTextForRichTextBox(RichTextBox richTextBox)
        {
            // This way of detecting whether the box is empty or not should be improved.
            // It will do for now though.
            bool hasText = !IsRichTextBoxEmpty(richTextBox);
            SetHasText(richTextBox, hasText);
            richTextBox.TraceVerbose("HasText set to {0}.", hasText);
        }

        private static void UpdateHasTextForPasswordBox(PasswordBox passwordBox)
        {
            bool hasText = passwordBox.SecurePassword.Length > 0;
            SetHasText(passwordBox, hasText);
            passwordBox.TraceVerbose("HasText set to {0}.", hasText);
        }

        private static bool IsRichTextBoxEmpty(RichTextBox richTextBox)
        {
            // Taken from:
            // https://stackoverflow.com/a/5825644/10018492
            // May need to be adapted a little bit, but for now, this seems to do what it's
            // supposed to do.
            if (richTextBox.Document.Blocks.Count == 0) return true;
            TextPointer contentStart = richTextBox.Document.ContentStart;
            TextPointer contentEnd = richTextBox.Document.ContentEnd;
            TextPointer startPointer = contentStart.GetNextInsertionPosition(LogicalDirection.Forward);
            TextPointer endPointer = contentEnd.GetNextInsertionPosition(LogicalDirection.Backward);

            return startPointer.CompareTo(endPointer) == 0;
        }
        
    }

}
