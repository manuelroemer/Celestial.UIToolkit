using System;
using System.Diagnostics;
using System.Windows;

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
        public static TraceSource ResourcesSource { get; } = CreateForToolkit("Resources");

        /// <summary>
        /// Gets a <see cref="TraceSource"/> which is used for any message that 
        /// relates to (custom) controls which are provided by the toolkit.
        /// </summary>
        public static TraceSource ControlsSource { get; } = CreateForToolkit("Controls");

        /// <summary>
        /// Gets a <see cref="TraceSource"/> which is used for any message that
        /// relates to animations.
        /// </summary>
        public static TraceSource AnimationSource { get; } = CreateForToolkit("Animation");

        /// <summary>
        /// Gets a <see cref="TraceSource"/> which is used for any message which comes from the
        /// members in the <see cref="Interactivity"/> and <see cref="Interactions"/>
        /// namespaces.
        /// </summary>
        public static TraceSource InteractivitySource { get; } = CreateForToolkit("Interactivity");

        internal static TraceSource CreateForToolkit(string sourceNameAddendum)
        {
            // This is the same as Create, with the difference that it adds a shared
            // Toolkit identifier name prefix.
            return Create("Celestial.UIToolkit." + sourceNameAddendum);
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


    // Internal helper extension methods for simplyfying the Tracing process.
    // Some of these methods are very specific to the UIToolkit, hence why they are internal.
    // Thanks to the [assembly:InternalsVisibleTo] attribute, these methods can be accessed
    // from the "Celestial.UIToolkit" assembly.
    internal static class TraceSourceExtensions
    {

        // This class may better be auto-generated, but I don't expect much change to it.
        // When the need arises, don't hesitate to create a T4 template though.

        private const int UnspecifiedTraceEventId = 0;

        #region Verbose

        public static void Verbose(this TraceSource traceSource, string message)
        {
            Verbose(traceSource, UnspecifiedTraceEventId, message);
        }

        public static void Verbose(this TraceSource traceSource, string format, params object[] args)
        {
            Verbose(traceSource, UnspecifiedTraceEventId, format, args);
        }

        public static void Verbose(this TraceSource traceSource, int id, string message)
        {
            traceSource.TraceEvent(TraceEventType.Verbose, id, message);
        }

        public static void Verbose(
            this TraceSource traceSource, int id, string format, params object[] args)
        {
            traceSource.TraceEvent(TraceEventType.Verbose, id, format, args);
        }

        //
        // Special helper methods for controls.
        //
        public static void TraceVerbose(
            this FrameworkElement frameworkElement, string message)
        {
            TraceVerbose(frameworkElement, message, null);
        }

        public static void TraceVerbose(
            this FrameworkElement frameworkElement, string format, params object[] args)
        {
            // When logging a message for a control, prepend the type name to the message,
            // so that we always know which control we are talking about.
            string feName = frameworkElement.GetType().Name;
            int uniqueId = frameworkElement.GetHashCode();
            format = $"{feName}: {format}";

            TraceSources.ControlsSource.Verbose(uniqueId, format, args);
        }

        #endregion

        #region Info

        public static void Info(this TraceSource traceSource, string message)
        {
            Info(traceSource, UnspecifiedTraceEventId, message);
        }

        public static void Info(this TraceSource traceSource, string format, params object[] args)
        {
            Info(traceSource, UnspecifiedTraceEventId, format, args);
        }

        public static void Info(this TraceSource traceSource, int id, string message)
        {
            traceSource.TraceEvent(TraceEventType.Information, id, message);
        }

        public static void Info(
            this TraceSource traceSource, int id, string format, params object[] args)
        {
            traceSource.TraceEvent(TraceEventType.Information, id, format, args);
        }

        //
        // Special helper methods for controls.
        //
        public static void TraceInfo(
            this FrameworkElement frameworkElement, string message)
        {
            TraceInfo(frameworkElement, message, null);
        }

        public static void TraceInfo(
            this FrameworkElement frameworkElement, string format, params object[] args)
        {
            // When logging a message for a control, prepend the type name to the message,
            // so that we always know which control we are talking about.
            string feName = frameworkElement.GetType().Name;
            int uniqueId = frameworkElement.GetHashCode();
            format = $"{feName}: {format}";

            TraceSources.ControlsSource.Info(uniqueId, format, args);
        }

        #endregion

        #region Warn

        public static void Warn(this TraceSource traceSource, string message)
        {
            Warn(traceSource, UnspecifiedTraceEventId, message);
        }

        public static void Warn(this TraceSource traceSource, string format, params object[] args)
        {
            Warn(traceSource, UnspecifiedTraceEventId, format, args);
        }

        public static void Warn(this TraceSource traceSource, int id, string message)
        {
            traceSource.TraceEvent(TraceEventType.Warning, id, message);
        }

        public static void Warn(
            this TraceSource traceSource, int id, string format, params object[] args)
        {
            traceSource.TraceEvent(TraceEventType.Warning, id, format, args);
        }

        //
        // Special helper methods for controls.
        //
        public static void TraceWarning(
            this FrameworkElement frameworkElement, string message)
        {
            TraceWarning(frameworkElement, message, null);
        }

        public static void TraceWarning(
            this FrameworkElement frameworkElement, string format, params object[] args)
        {
            // When logging a message for a control, prepend the type name to the message,
            // so that we always know which control we are talking about.
            string feName = frameworkElement.GetType().Name;
            int uniqueId = frameworkElement.GetHashCode();
            format = $"{feName}: {format}";

            TraceSources.ControlsSource.Warn(uniqueId, format, args);
        }

        #endregion

        #region Error

        public static void Error(this TraceSource traceSource, string message)
        {
            Error(traceSource, UnspecifiedTraceEventId, message);
        }

        public static void Error(this TraceSource traceSource, string format, params object[] args)
        {
            Error(traceSource, UnspecifiedTraceEventId, format, args);
        }

        public static void Error(this TraceSource traceSource, int id, string message)
        {
            traceSource.TraceEvent(TraceEventType.Error, id, message);
        }

        public static void Error(
            this TraceSource traceSource, int id, string format, params object[] args)
        {
            traceSource.TraceEvent(TraceEventType.Error, id, format, args);
        }

        //
        // Special helper methods for controls.
        //
        public static void TraceError(
            this FrameworkElement frameworkElement, string message)
        {
            TraceError(frameworkElement, message, null);
        }

        public static void TraceError(
            this FrameworkElement frameworkElement, string format, params object[] args)
        {
            // When logging a message for a control, prepend the type name to the message,
            // so that we always know which control we are talking about.
            string feName = frameworkElement.GetType().Name;
            int uniqueId = frameworkElement.GetHashCode();
            format = $"{feName}: {format}";

            TraceSources.ControlsSource.Error(uniqueId, format, args);
        }

        #endregion

    }

}
