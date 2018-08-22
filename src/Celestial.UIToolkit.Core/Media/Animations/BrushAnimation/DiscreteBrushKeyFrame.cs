using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Animates from the <see cref="Brush"/> value of the previous key frame to
    /// its own <see cref="IKeyFrame.Value"/> using discrete interpolation.
    /// </summary>
    public class DiscreteBrushKeyFrame : DiscreteKeyFrameBase<Brush>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteBrushKeyFrame"/> class
        /// with default values.
        /// </summary>
        public DiscreteBrushKeyFrame()
            : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteBrushKeyFrame"/> class
        /// with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        public DiscreteBrushKeyFrame(Brush value)
            : base(value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteBrushKeyFrame"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        /// <param name="keyTime">A <see cref="KeyTime"/> value which is associated with this key frame.</param>
        public DiscreteBrushKeyFrame(Brush value, KeyTime keyTime)
            : base(value, keyTime) { }
    
        /// <summary>
        /// Creates a new instance of the <see cref="DiscreteBrushKeyFrame"/> class.
        /// </summary>
        /// <returns>A new <see cref="DiscreteBrushKeyFrame"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new DiscreteBrushKeyFrame();

    }

}
