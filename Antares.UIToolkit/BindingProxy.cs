using System.Windows;

namespace Antares.UIToolkit
{

    // Taken from https://stackoverflow.com/a/42488470/10018492 and
    //            https://stackoverflow.com/a/22074985/424129.
    // Bless the persons who came up with this..

    /// <summary>
    /// A class which is used to allow bindings in places where they wouldn't normally be possible,
    /// by creating a proxy between the two parties.
    /// </summary>
    public class BindingProxy : Freezable
    {

        /// <summary>
        /// Identifies the <see cref="Data"/> dependency property. 
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data", typeof(object), typeof(BindingProxy));

        /// <summary>
        /// Gets or sets the data which this object is proxying.
        /// </summary>
        public object Data
        {
            get => GetValue(DataProperty); 
            set => SetValue(DataProperty, value); 
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
            return "Data=\"" + (this.Data?.ToString() ?? "null") + "\"";
        }

    }

}
