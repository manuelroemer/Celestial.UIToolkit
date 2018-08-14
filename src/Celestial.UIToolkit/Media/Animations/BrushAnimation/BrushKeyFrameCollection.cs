using System.Collections.Generic;
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
        /// Initializes a new, empty instance of the <see cref="BrushKeyFrameCollection"/> class.
        /// </summary>
        public BrushKeyFrameCollection() { }

        /// <summary>
        /// Initializes a new, empty instance of the <see cref="BrushKeyFrameCollection"/> class
        /// with the specified initial capacity.
        /// </summary>
        /// <param name="capacity">An initial capacity of elements.</param>
        public BrushKeyFrameCollection(int capacity)
            : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrushKeyFrameCollection"/> class
        /// with the specified <paramref name="keyFrames"/> as a source.
        /// </summary>
        /// <param name="keyFrames">
        /// A set of key frames which are copied into this collection.
        /// </param>
        public BrushKeyFrameCollection(IEnumerable<KeyFrameBase<Brush>> keyFrames)
            : base(keyFrames) { }

        /// <summary>
        /// Creates a new instance of the <see cref="BrushKeyFrameCollection"/> class.
        /// </summary>
        /// <returns>A new <see cref="BrushKeyFrameCollection"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new BrushKeyFrameCollection();
        
    }

}
