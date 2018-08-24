using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    public partial class SplitView : ContentControl
    {

        static SplitView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SplitView), new FrameworkPropertyMetadata(typeof(SplitView)));
        }

        public SplitView()
        {
            Loaded += SplitView_Loaded;
        }

        private void SplitView_Loaded(object sender, RoutedEventArgs e)
        {
            EnterCurrentDisplayModeVisualState(false);
        }

        private static void DisplayModeProperty_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Called whenever a property is changed, that updates the DisplayModes visual states.
            var self = (SplitView)d;
            self.EnterCurrentDisplayModeVisualState();
        }

        private static void IsPaneOpen_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (SplitView)d;
            self.UpdateCurrentPaneToggleState();
        }

        private void UpdateCurrentPaneToggleState()
        {
            // IsPaneOpen already holds the new, desired value. -> Transition to these states.
            if (IsPaneOpen)
                OpenPane();
            else
                ClosePane();
        }
        
        private void OpenPane()
        {
            RaisePaneOpening();
            EnterCurrentDisplayModeVisualState();
            RaisePaneOpened();
        }

        private void ClosePane()
        {
            RaisePaneClosing();
            EnterCurrentDisplayModeVisualState();
            RaisePaneClosed();
        }
        
    }

}
