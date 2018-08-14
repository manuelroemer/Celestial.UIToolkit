using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// A freezable collection which is able to hold key frames of <see cref="Brush"/> objects.
    /// </summary>
    public class BrushKeyFrameCollection : FreezableCollection<KeyFrameBase<Brush>>
    {

        /// <summary>
        /// Creates a new instance of the <see cref="BrushKeyFrameCollection"/> class.
        /// </summary>
        /// <returns>A new <see cref="BrushKeyFrameCollection"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new BrushKeyFrameCollection();
        
    }

}
