using System.Windows.Data;

namespace Celestial.UIToolkit.Xaml
{
    // The purpose of these classes is to make XAML Code more compact.
    // For instance, instead of
    //
    //     Value="{Binding MyProperty, RelativeSource={RelativeSource TemplatedParent}}
    //
    // we can write
    //
    //     Value="{c:TemplatedParentBinding MyProperty}
    //
    // which, as you can see, is about half the size.


    /// <summary>
    ///     An otherwise normal <see cref="Binding"/> whose <see cref="Binding.RelativeSource"/>
    ///     property is set to <see cref="RelativeSource.Self"/>.
    /// </summary>
    public class SelfBindingExtension : Binding
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelfBindingExtension"/> class.
        /// </summary>
        public SelfBindingExtension()
            : this(null) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelfBindingExtension"/> class.
        /// </summary>
        /// <param name="path">
        ///     The initial <see cref="Binding.Path"/> for the binding.
        /// </param>
        public SelfBindingExtension(string path)
            : base(path)
        {
            RelativeSource = RelativeSource.Self;
        }

    }

    /// <summary>
    ///     An otherwise normal <see cref="Binding"/> whose <see cref="Binding.RelativeSource"/>
    ///     property is set to <see cref="RelativeSource.TemplatedParent"/>.
    /// </summary>
    public class TemplatedParentBindingExtension : Binding
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="TemplatedParentBindingExtension"/> class.
        /// </summary>
        public TemplatedParentBindingExtension()
            : this(null) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TemplatedParentBindingExtension"/> class.
        /// </summary>
        /// <param name="path">
        ///     The initial <see cref="Binding.Path"/> for the binding.
        /// </param>
        public TemplatedParentBindingExtension(string path)
            : base(path)
        {
            RelativeSource = RelativeSource.TemplatedParent;
        }

    }

}
