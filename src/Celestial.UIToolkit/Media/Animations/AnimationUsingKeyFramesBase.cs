using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// A base class for an animation using key frames.
    /// </summary>
    /// <typeparam name="T">The type of animated object.</typeparam>
    /// <typeparam name="TKeyFrameCollection">The type of collection which holds the animation's key frames.</typeparam>
    [ContentProperty(nameof(KeyFrames))]
    public abstract class AnimationUsingKeyFramesBase<T, TKeyFrameCollection> : AnimationBase<T>, IAddChild, IKeyFrameAnimation
        where TKeyFrameCollection : Freezable, IList, new()
    {

        private TKeyFrameCollection _keyFrames;
        /// <summary>
        /// Gets or sets the list of key frames which define the animation.
        /// </summary>
        public TKeyFrameCollection KeyFrames
        {
            get
            {
                if (_keyFrames == null)
                {
                    if (this.IsFrozen)
                    {
                        _keyFrames = new TKeyFrameCollection();
                    }
                    else
                    {
                        this.KeyFrames = new TKeyFrameCollection();
                    }
                }
                return _keyFrames;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (ReferenceEquals(value, _keyFrames)) return;
                this.WritePreamble();

                this.OnFreezablePropertyChanged(_keyFrames, value);
                _keyFrames = value;
                this.WritePostscript();
            }
        }

        /// <summary>
        /// Returns a value indicating whether the <see cref="KeyFrames"/> property
        /// should be serialized or not.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <see cref="KeyFrames"/> should be serialized; <c>false</c> if not.
        /// </returns>
        public bool ShouldSerializeKeyFrames()
        {
            this.ReadPreamble();
            return _keyFrames != null && _keyFrames.Count > 0;
        }

        IList IKeyFrameAnimation.KeyFrames
        {
            get => this.KeyFrames;
            set => this.KeyFrames = (TKeyFrameCollection)value;
        }

        void IAddChild.AddChild(object child) => this.AddChild(child);

        void IAddChild.AddText(string text) => this.AddText(text);

        /// <summary>
        /// Adds a child to the key frame animation.
        /// </summary>
        /// <param name="child">The child to be added.</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException">
        /// Thrown if the <paramref name="child"/> is not of type <typeparamref name="T"/>.
        /// </exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void AddChild(object child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (!(child is T))
                throw new ArgumentException($"The child must be of type {typeof(T).FullName}.", nameof(child));
        }

        /// <summary>
        /// Adds a textual child to the key frame animation.
        /// By default, this throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="text">The textual child to be added.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void AddText(string text)
        {
            throw new InvalidOperationException(
                $"The {this.GetType().FullName} doesn't support adding textual children.");
        }

        private void ResolveKeyTimes()
        {
            int frameCount = _keyFrames?.Count ?? 0;
            throw new NotImplementedException();
        }
        
    }

}
