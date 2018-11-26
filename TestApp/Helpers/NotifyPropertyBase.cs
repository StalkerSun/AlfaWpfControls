using System.ComponentModel;



namespace TestApp.Helpers
{
    public class NotifyPropertyBase : INotifyPropertyChanged
    {
        public NotifyPropertyBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
