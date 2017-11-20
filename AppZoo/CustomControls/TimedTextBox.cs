using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace AppZoo.CustomControls
{
    public sealed class TimedTextBox : TextBox
    {
        public int Delay
        {
            get { return (int)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(int), typeof(TimedTextBox), new PropertyMetadata(0, OnDelayChanged));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(TimedTextBox), new PropertyMetadata(0));

        private DispatcherTimer _timer { get; set; }

        public TimedTextBox()
        {
            this.DefaultStyleKey = typeof(TimedTextBox);
            TextChanged += TimedTextBox_TextChanged;
            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, Delay);
        }

        private void _timer_Tick(object sender, object e)
        {
            Value = Text;
        }

        private void TimedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                _timer.Start();
            }
            else
            {
                _timer.Start();
            }
        }

        private static void OnDelayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((TimedTextBox)d)._timer.Interval = new TimeSpan(0, 0, (int)e.NewValue);
        }
    }
}
