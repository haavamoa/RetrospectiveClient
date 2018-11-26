using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Retrospective.Clients.WPF.Utils
{
    public class FocusSetterBehavior
    {
        private static DispatcherTimer m_focusTimer;
        private static UIElement m_focusElement;

        public static readonly DependencyProperty DelayProperty = DependencyProperty.RegisterAttached(
            "Delay",
            typeof(int),
            typeof(FocusSetterBehavior),
            new PropertyMetadata(default(int)));

        public static void SetDelay(DependencyObject element, int value)
        {
            element.SetValue(DelayProperty, value);
        }

        public static int GetDelay(DependencyObject element)
        {
            return (int)element.GetValue(DelayProperty);
        }

        #region IsFocused
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
            "IsFocused", typeof(bool), typeof(FocusSetterBehavior), new PropertyMetadata(false, null, OnIsFocusedChanged));

        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        private static object OnIsFocusedChanged(DependencyObject d, object e)
        {
            var uiElement = (UIElement)d;
            if ((bool)e)
            {
                if (!uiElement.Focusable)
                {
                    uiElement.Focusable = true;
                }

                if (GetDelay(d) > 0)
                {
                    var frameworkElement = d as FrameworkElement;
                    if (frameworkElement != null)
                    {
                        frameworkElement.Unloaded += FrameworkElementOnUnloaded;
                        m_focusElement = uiElement;

                        DelayedSetFocus(d);
                        return (bool)e;
                    }                   
                }

                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => uiElement.Focus()), DispatcherPriority.Render);
            }

            return (bool)e;
        }

        private static void FrameworkElementOnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            m_focusElement = null;
            StopFocusTimer();
        }

        private static void DelayedSetFocus(DependencyObject d)
        {
            if (m_focusTimer == null)
            {
                m_focusTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(GetDelay(d)) };
                m_focusTimer.Tick += FocusTimerTick;
                m_focusTimer.Start();
            }
        }

        private static void FocusTimerTick(object sender, EventArgs e)
        {
            if (m_focusElement != null)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(
                    () =>
                    {
                        m_focusElement.Focus();
                        m_focusElement = null;
                    }), DispatcherPriority.Render);
            }

            StopFocusTimer();
        }

        private static void StopFocusTimer()
        {
            if (m_focusTimer != null)
            {
                m_focusTimer.Stop();
                m_focusTimer = null;
            }
        }

        #endregion
        
        #region FocusFirst
        public static readonly DependencyProperty FocusFirstProperty = DependencyProperty.RegisterAttached(
            "FocusFirst", typeof(bool), typeof(FocusSetterBehavior), new PropertyMetadata(false, null, OnFocusFirstPropertyChanged));

        public static bool GetFocusFirst(Control control)
        {
            return (bool)control.GetValue(FocusFirstProperty);
        }

        public static void SetFocusFirst(Control control, bool value)
        {
            control.SetValue(FocusFirstProperty, value);
        }

        private static object OnFocusFirstPropertyChanged(DependencyObject d, object e)
        { 
            var uiElement = (UIElement)d;
   
            if ((bool)e)
            {
                var control = d as Control;
                if (control != null)
                {
                    control.Loaded += ControlOnLoaded;
                    control.Unloaded -= ControlOnLoaded;
                }

                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next))));
            }
            else
            {
                var control = d as Control;
                if (control != null)
                {
                    control.Loaded -= ControlOnLoaded;
                }
            }

            return (bool)e;
        }

        private static void ControlOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var control = sender as Control;
            if (control != null)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => control.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next))));
            }
        }
        #endregion
    }
}
