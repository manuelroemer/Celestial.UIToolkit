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
        public static ICommand MinimizeCommand { get; } = new MinimizeCommandImpl();

        /// <summary>
        /// Gets a command which can be used to maximize or restore a window.
        /// For this command to work, you need to bind the CommandParameter to a
        /// <see cref="Window"/> instance.
        /// </summary>
        public static ICommand MaximizeCommand { get; } = new MaximizeCommandImpl();

        /// <summary>
        /// Gets a command which can be used to close a window.
        /// For this command to work, you need to bind the CommandParameter to a
        /// <see cref="Window"/> instance.
        /// </summary>
        public static ICommand CloseCommand { get; } = new CloseCommandImpl();

        /// <summary>
        /// Gets a command which can be used to show a window's system menu.
        /// For this command to work, you need to bind the CommandParameter to a
        /// <see cref="Window"/> instance.
        /// </summary>
        public static ICommand ShowSystemMenuCommand { get; } = new ShowSystemShowSystemMenuCommandImpl();

        /// <summary>
        /// Gets a command which can be used to show a window.
        /// For this command to work, you need to bind the CommandParameter to a
        /// <see cref="Window"/> instance.
        /// </summary>
        public static ICommand ShowCommand { get; } = new ShowCommandImpl();

        /// <summary>
        /// Gets a command which can be used to show a window as a dialog.
        /// For this command to work, you need to bind the CommandParameter to a
        /// <see cref="Window"/> instance.
        /// </summary>
        public static ICommand ShowDialogCommand { get; } = new ShowDialogCommandImpl();

        private abstract class WindowCommand : ICommand
        {

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter)
            {
                // This check should intuitively go into CanExecute, but there are situations in
                // which CanExecute doesn't update.
                // Thus, the command gets flagged as not executable, even though it is.
                // By doing the check here, we are on the save side.
                if (parameter is Window window)
                {
                    ExecuteCore(window);
                }
            }

            protected abstract void ExecuteCore(Window window);

            protected virtual void OnCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

        }

        private class CloseCommandImpl : WindowCommand
        {

            protected override void ExecuteCore(Window window)
            {
                SystemCommands.CloseWindow(window);
            }

        }

        private class MinimizeCommandImpl : WindowCommand
        {
            protected override void ExecuteCore(Window window)
            {
                if (window.WindowState == WindowState.Minimized)
                    SystemCommands.RestoreWindow(window);
                else
                    SystemCommands.MinimizeWindow(window);
            }
        }

        private class MaximizeCommandImpl : WindowCommand
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

        private class ShowCommandImpl : WindowCommand
        {
            protected override void ExecuteCore(Window window)
            {
                window.Show();
            }
        }

        private class ShowDialogCommandImpl : WindowCommand
        {
            protected override void ExecuteCore(Window window)
            {
                window.ShowDialog();
            }
        }

    }

}
