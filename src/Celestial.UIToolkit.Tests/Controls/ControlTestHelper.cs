using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Celestial.UIToolkit.Tests.Controls
{

    /// <summary>
    /// Provides static helper methods for unit testing controls.
    /// Most of these methods provide workarounds for WPF limitations in unit test projects.
    /// </summary>
    public static class ControlTestHelper
    {

        public static void ChangeActualSize(
            this FrameworkElement fe, double width, double height)
        {
            fe.Width = width;
            fe.Height = height;
            MeasureArrange(fe, width, height);
        }

        public static void MeasureArrange(
            this FrameworkElement fe, double width, double height)
        {
            fe.Measure(new Size(width, height));
            fe.Arrange(new Rect(0, 0, width, height));
        }

        /// <summary>
        /// Changes the element's actual size and ensures that the SizeChanged event is raised.
        /// This method includes a timeout which can be set with 
        /// <paramref name="msecsTimeout"/>. Depending on this number, a test might have to be
        /// ignored.
        /// </summary>
        public static void ChangeActualSizeAndRaiseEvent(
            this FrameworkElement fe, double width, double height, int msecsTimeout = 500)
        {
            // For changing the actual size of a control, force the Measure/Arrange layout cycle
            // twice. Wait in between to give WPF time to queue the SizeChanged event.

            // Set the element's initial size to a value which guarantees a SizeChanged event.
            double initialWidth = width * 2 + 100;
            double initialHeight = height * 2 + 100;

            ChangeActualSize(fe, initialWidth, initialHeight);
            ChangeActualSize(fe, width, height);
            DispatcherWait(msecsTimeout);
        }

        public static void DispatcherWait(double msecsTimeout)
            => DispatcherWait(TimeSpan.FromMilliseconds(msecsTimeout));

        public static void DispatcherWait(TimeSpan timeout)
        {
            // Taken from https://stackoverflow.com/a/6852078
            // for testing events like SizeChanged.
            // I didn't find a better workaround.
            var frame = new DispatcherFrame();
            new Thread(() =>
            {
                Thread.Sleep(timeout);
                frame.Continue = false;
            }).Start();
            Dispatcher.PushFrame(frame);
        }

    }

}
