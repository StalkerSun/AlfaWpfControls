using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using TestApp.Helpers;

namespace TestApp.ViewModels
{
    public class VmATimerIndicator : NotifyPropertyBase
    {


        public event EventHandler OnStart;
        public event EventHandler OnStop;
        public event EventHandler OnRestart;

        private string _strDuration;

        public string StrDuration
        {
            get { return _strDuration; }
            set
            {
                _strDuration = value;

                OnPropertyChanged("StrDuration");

                var pars = Int32.TryParse(value, out int b);

                Duration111 = new TimeSpan(0, pars ? b : 0, 0);

            }
        }

        private TimeSpan _duration;

        public TimeSpan Duration111
        {
            get { return _duration; }
            set
            {
                _duration = value;

                OnPropertyChanged("Duration111");
            }
        }




        #region StartCommand

        public RelayCommand StartCommand
        {
            get { return new RelayCommand(ExecuteStartCommand, () => true); }
        }

        private void ExecuteStartCommand()
        {
            var handler = OnStart;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }


        #endregion

        #region StopCommand

        public RelayCommand StopCommand
        {
            get { return new RelayCommand(ExecuteStopCommand, () => true); }
        }

        private void ExecuteStopCommand()
        {
            var handler = OnStop;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }



        #endregion

        #region ReStartCommand

        public RelayCommand ReStartCommand
        {
            get { return new RelayCommand(ExecuteReStartCommand, () => true); }
        }

        private void ExecuteReStartCommand()
        {
            var handler = OnRestart;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }



        #endregion

        public VmATimerIndicator()
        {
            Duration111 = new TimeSpan(0, 0, 0);
            StrDuration = "3";
        }


        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            var res = regex.IsMatch(e.Text);

            if (res)
            {
                e.Handled = res;
            }

            var text = ( ( TextBox ) sender ).Text;

            var resInt = Int32.TryParse(text + e.Text, out int a);

            e.Handled = !( resInt && a <= 99 && a >= 1 );
        }

    }
}
