using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    ///     A <see cref="ContentControl"/> which depicts a card.
    ///     In addition to the the <see cref="ContentControl.Content"/>,
    ///     the <see cref="Card"/> provides a set of additional content properties
    ///     like <see cref="Title"/> and <see cref="MediaContent"/> to organize the
    ///     content into structured parts.
    ///     This allows control templates to change the arrangement of the card's
    ///     different elements, while giving the user full control over what the 
    ///     content looks like.
    /// </summary>
    /// <remarks>
    ///     At its core, a card is nothing else but a glorified border which hosts
    ///     content.
    ///     In terms of design language, a card is a contained, independent unit
    ///     which displays and organizes content.
    ///     Learn more about cards at: 
    ///     https://material.io/design/components/cards.html#anatomy
    /// </remarks>
    public partial class Card : ContentControl
    {

        // Most properties of this class are auto-generated.
        // See the corresponding .tt file.

        static Card()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
        }

    }

}
