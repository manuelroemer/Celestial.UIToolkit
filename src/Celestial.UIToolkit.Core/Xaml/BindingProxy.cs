using System.Diagnostics;
using System.Windows;

namespace Celestial.UIToolkit.Xaml
{

    // Taken from https://stackoverflow.com/a/42488470/10018492 and
    //            https://stackoverflow.com/a/22074985/424129.
    // Bless the persons who came up with this..

    /// <summary>
    /// A class which is used to allow bindings in places where they wouldn't normally be possible,
    /// by creating a proxy between the two parties.
    /// </summary>
    [DebuggerDisplay(nameof(Data) + ": {" + nameof(Data) + "}")]
    public class BindingProxy : Freezable
    {

        /// <summary>
        /// Identifies the <see cref="Data"/> dependency property. 
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            nameof(Data), typeof(object), typeof(BindingProxy), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the data which this object is proxying.
        /// </summary>
        public object Data
        {
            get => GetValue(DataProperty); 
            set => SetValue(DataProperty, value); 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingProxy"/> class.
        /// </summary>
        public BindingProxy() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingProxy"/> class with the
        /// specified <paramref name="data"/>.
        /// </summary>
        /// <param name="data">The data which this object is proxying.</param>
        public BindingProxy(object data)
        {
            Data = data;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BindingProxy"/> class.
        /// </summary>
        /// <returns>A new instance of the <see cref="BindingProxy"/> class.</returns>
        protected override Freezable CreateInstanceCore() => new BindingProxy();

        /// <summary>
        /// Returns a string representation of the proxy's <see cref="Data"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the proxy's data.</returns>
        public override string ToString()
        {
            return Data?.ToString() ?? "";
        }

    }

}
