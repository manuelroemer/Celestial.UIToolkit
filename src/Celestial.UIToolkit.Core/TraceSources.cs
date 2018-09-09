using System;
using System.Diagnostics;

namespace Celestial.UIToolkit
{

    /// <summary>
    /// Provides access to static <see cref="TraceSource"/> instances which are used by the
    /// toolkit to trace information.
    /// This class has the same intent as the <see cref="PresentationTraceSources"/> class,
    /// but is created separately, so that WPF's trace messages are left untouched.
    /// </summary>
    public static class TraceSources
    {

        /// <summary>
        /// Gets a <see cref="TraceSource"/> which is used for any message that
        /// relates to (XAML) markup.
        /// </summary>
        public static TraceSource MarkupSource { get; } = CreateForToolkit("Markup");

        /// <summary>
        /// Gets a <see cref="TraceSource"/> which is used for any message that
        /// relates to visual states.
        /// </summary>
        /// <remarks>
        /// This is mainly used by the custom visual state manager and only added 
        /// to not clutter other trace sources.
        /// </remarks>
        public static TraceSource VisualStateSource { get; } = CreateForToolkit("VisualState");

        /// <summary>
        /// Gets a <see cref="TraceSource"/> which is used for any message that
        /// relates to resources.
        /// </summary>
        public static TraceSource ResourceSource { get; } = CreateForToolkit("Resources");

        /// <summary>
        /// Gets a <see cref="TraceSource"/> which is used for any message that 
        /// relates to (custom) controls which are provided by the toolkit.
        /// </summary>
        public static TraceSource ControlSource { get; } = CreateForToolkit("Controls");

        /// <summary>
        /// Gets a <see cref="TraceSource"/> which is used for any message that
        /// relates to animations.
        /// </summary>
        public static TraceSource AnimationSource { get; } = CreateForToolkit("Animation");

        internal static TraceSource CreateForToolkit(string sourceNameAddendum)
        {
            // This is the same as Create, with the difference that it adds a shared
            // Toolkit identifier name prefix.
            return Create("Celestial.UIToolkit.");
        }

        /// <summary>
        /// Creates a new, configured <see cref="TraceSource"/> with the specified name.
        /// </summary>
        /// <param name="sourceName">
        /// The name of the <see cref="TraceSource"/> to be created.
        /// </param>
        /// <returns>
        /// A new <see cref="TraceSource"/> instance with the specified 
        /// <paramref name="sourceName"/>.
        /// </returns>
        public static TraceSource Create(string sourceName)
        {
            if (string.IsNullOrEmpty(sourceName))
                throw new ArgumentNullException(nameof(sourceName));

            var traceSource = new TraceSource(sourceName);

            // If we are debugging, always show warnings/errors, even if disabled originally.
            if (traceSource.Switch.Level == SourceLevels.Off &&
                Debugger.IsAttached)
            {
                // In the ReferenceSource, there is a try/catch around the same assignment.
                // I don't know if anything can ever happen here, but it doesn't hurt to be careful.
                try
                {
                    traceSource.Switch.Level = SourceLevels.Warning;
                }
                finally { }
            }

            return traceSource;
        }

    }

}
