using System;
using System.Text.RegularExpressions;
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


            }
        }

        private TimeSpan _duration;

        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;

                OnPropertyChanged("Duration");
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

        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]+");

            var res = regex.IsMatch(e.Text);
            e.Handled = !res;
        }

    }
}
