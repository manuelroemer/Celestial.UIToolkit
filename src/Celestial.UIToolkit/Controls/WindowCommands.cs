using System;
using System.Windows;
using System.Windows.Input;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Provides static <see cref="ICommand"/> implementations which can be used to add
    /// Minimize, Maximize, Restore and Close handlers to custom window caption buttons.
    /// </summary>
    public static class WindowCommands
    {

        /// <summary>
        /// Gets a command which can be used to minimize or restore a window.
        /// For this command to work, you need to bind the CommandParameter to a
        /// <see cref="Window"/> instance.
        /// </summary>
        public static ICommand MinimizeWindowCommand { get; } = new MinimizeWindowCommandImpl();

        /// <summary>
        /// Gets a command which can be used to maximize or restore a window.
        /// For this command to work, you need to bind the CommandParameter to a
        /// <see cref="Window"/> instance.
        /// </summary>
        public static ICommand MaximizeWindowCommand { get; } = new MaximizeWindowCommandImpl();

        /// <summary>
        /// Gets a command which can be used to close a window.
        /// For this command to work, you need to bind the CommandParameter to a
        /// <see cref="Window"/> instance.
        /// </summary>
        public static ICommand CloseWindowCommand { get; } = new CloseWindowCommandImpl();

        /// <summary>
        /// Gets a command which can be used to show a window's system menu.
        /// For this command to work, you need to bind the CommandParameter to a
        /// <see cref="Window"/> instance.
        /// </summary>
        public static ICommand ShowSystemMenuCommand { get; } = new ShowSystemShowSystemMenuCommandImpl();

        private abstract class WindowCommand : ICommand
        {

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => parameter is Window;

            public void Execute(object parameter)
            {
                ExecuteCore((Window)parameter);
            }

            protected abstract void ExecuteCore(Window window);

            protected virtual void OnCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

        }

        private class CloseWindowCommandImpl : WindowCommand
        {

            protected override void ExecuteCore(Window window)
            {
                SystemCommands.CloseWindow(window);
            }

        }

        private class MinimizeWindowCommandImpl : WindowCommand
        {

            protected override void ExecuteCore(Window window)
            {
                if (window.WindowState == WindowState.Minimized)
                    SystemCommands.RestoreWindow(window);
                else
                    SystemCommands.MinimizeWindow(window);
            }

        }

        private class MaximizeWindowCommandImpl : WindowCommand
        {

            protected override void ExecuteCore(Window window)
            {
                if (window.WindowState == WindowState.Maximized)
                    SystemCommands.RestoreWindow(window);
                else
                    SystemCommands.MaximizeWindow(window);
            }

        }

        private class ShowSystemShowSystemMenuCommandImpl : WindowCommand
        {

            protected override void ExecuteCore(Window window)
            {
                SystemCommands.ShowSystemMenu(
                    window, 
                    new Point(
                        Mouse.GetPosition(window).X + window.Left, 
                        Mouse.GetPosition(window).Y + window.Top
                    )
                );
            }

        }

    }

}
