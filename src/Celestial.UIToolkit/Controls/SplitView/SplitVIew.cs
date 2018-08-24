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
            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            EnterCurrentPaneToggleVisualState(false);
            EnterCurrentDisplayModeVisualState(false);
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
            EnterCurrentPaneToggleVisualState();
            RaisePaneOpened();
        }

        private void ClosePane()
        {
            RaisePaneClosing();
            EnterCurrentPaneToggleVisualState();
            RaisePaneClosed();
        }

        private static void DisplayMode_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (SplitView)d;
            self.EnterCurrentDisplayModeVisualState();
        }
        
    }

}
