using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Definesthe different display modes of a <see cref="SplitView"/> control.
    /// </summary>
    public enum SplitViewDisplayMode
    {

        /// <summary>
        /// When opened, the pane hovers over the content.
        /// When closed, the pane is no longer visible.
        /// </summary>
        Overlay,

        /// <summary>
        /// When opened, the pane is displayed next to the content and takes up space.
        /// When closed, the pane is no longer visible and thus doesn't take up space anymore.
        /// </summary>
        Inline,

        /// <summary>
        /// The pane always takes up a small amount of space.
        /// When opened, the pane expands to the full size. The expanded area hovers over the 
        /// content.
        /// When closed, the pane is reduced to the strip.
        /// </summary>
        CompactOverlay,

        /// <summary>
        /// When opened, the pane expands to the full size and takes up the required space.
        /// When closed, the pane is reduced to a strip which still takes up space.
        /// This is essentially the same as <see cref="CompactOverlay"/>, with the only difference
        /// being that the pane always takes up space.
        /// </summary>
        CompactInline

    }

}
