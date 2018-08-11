using System;
using System.Windows;

namespace Celestial.UIToolkit
{

    /// <summary>
    /// An abstract extension of the <see cref="Freezable"/> class,
    /// providing additional quality-of-life methods for implementing custom freezable
    /// objects.
    /// </summary>
    public abstract class ExtendedFreezable : Freezable
    {

        /// <summary>
        /// Creates an artifical scope for reading properties around the specified
        /// <paramref name="scopeAction"/>, by calling <see cref="Freezable.ReadPreamble"/> and then
        /// executing the specified <paramref name="scopeAction"/>.
        /// </summary>
        /// <param name="scopeAction">
        /// An <see cref="Action"/> to be executed.
        /// </param>
        protected void EnterReadScope(Action scopeAction)
        {
            this.ReadPreamble();
            scopeAction?.Invoke();
        }

        /// <summary>
        /// Creates an artificial scope for reading properties around the specified
        /// <paramref name="scopeFunction"/>, by calling <see cref="Freezable.ReadPreamble"/> and then
        /// executing the specified <paramref name="scopeFunction"/> and returning the result.
        /// </summary>
        /// <typeparam name="T">The type of the function's return value.</typeparam>
        /// <param name="scopeFunction">
        /// A function to be executed.
        /// </param>
        /// <returns>
        /// The result of the function.
        /// </returns>
        /// <exception cref="ArgumentNullException" />
        protected T EnterReadScope<T>(Func<T> scopeFunction)
        {
            if (scopeFunction == null) throw new ArgumentNullException(nameof(scopeFunction));
            this.ReadPreamble();
            return scopeFunction();
        }

        /// <summary>
        /// Creates an artificial scope for writing properties around the specified
        /// <paramref name="scopeAction"/>, by calling <see cref="Freezable.WritePreamble"/>,
        /// executing the specified <paramref name="scopeAction"/> and then calling
        /// <see cref="Freezable.WritePostscript"/>.
        /// </summary>
        /// <param name="scopeAction">
        /// An <see cref="Action"/> to be executed.
        /// </param>
        protected void EnterWriteScope(Action scopeAction)
        {
            this.EnterWriteScope(scopeAction, false);
        }

        /// <summary>
        /// Creates an artificial scope for writing properties around the specified
        /// <paramref name="scopeAction"/>, by calling <see cref="Freezable.WritePreamble"/>,
        /// executing the specified <paramref name="scopeAction"/> and then calling
        /// <see cref="Freezable.WritePostscript"/> (if <paramref name="suppressWritePostscript"/> is
        /// <c>false</c>).
        /// </summary>
        /// <param name="scopeAction">
        /// An <see cref="Action"/> to be executed.
        /// </param>
        /// <param name="suppressWritePostscript">
        /// A value indicating whether <see cref="Freezable.WritePostscript"/>
        /// should be called after executing <paramref name="scopeAction"/>.
        /// </param>
        protected void EnterWriteScope(Action scopeAction, bool suppressWritePostscript)
        {
            this.WritePreamble();
            scopeAction?.Invoke();
            if (!suppressWritePostscript)
                this.WritePostscript();
        }

        /// <summary>
        /// Creates an artificial scope for writing properties around the specified
        /// <paramref name="scopeFunction"/>, by calling <see cref="Freezable.WritePreamble"/>,
        /// executing the specified <paramref name="scopeFunction"/>, calling
        /// <see cref="Freezable.WritePostscript"/> and finally returning the 
        /// <paramref name="scopeFunction"/>'s result.
        /// </summary>
        /// <param name="scopeFunction">
        /// A function to be executed.
        /// </param>
        /// <returns>
        /// The result of the function.
        /// </returns>
        /// <exception cref="ArgumentNullException" />
        protected T EnterWriteScope<T>(Func<T> scopeFunction)
        {
            return this.EnterWriteScope(scopeFunction, false);
        }

        /// <summary>
        /// Creates an artificial scope for writing properties around the specified
        /// <paramref name="scopeFunction"/>, by calling <see cref="Freezable.WritePreamble"/>,
        /// executing the specified <paramref name="scopeFunction"/>, calling
        /// <see cref="Freezable.WritePostscript"/> (if <paramref name="suppressWritePostscript"/> is
        /// <c>false</c>) and finally returning the <paramref name="scopeFunction"/>'s result.
        /// </summary>
        /// <param name="scopeFunction">
        /// A function to be executed.
        /// </param>
        /// <param name="suppressWritePostscript">
        /// A value indicating whether <see cref="Freezable.WritePostscript"/>
        /// should be called after executing <paramref name="scopeFunction"/>.
        /// </param>
        /// <returns>
        /// The result of the function.
        /// </returns>
        /// <exception cref="ArgumentNullException" />
        protected T EnterWriteScope<T>(Func<T> scopeFunction, bool suppressWritePostscript)
        {
            if (scopeFunction == null) throw new ArgumentNullException(nameof(scopeFunction));
            this.WritePreamble();
            var result = scopeFunction();
            if (!suppressWritePostscript)
                this.WritePostscript();
            return result;
        }

    }

}
