using System;
using System.Windows;

namespace AlfaWpfControls.Helpers
{
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
                nameof(Data),
                typeof(object),
                typeof(BindingProxy),
                new UIPropertyMetadata(null, propChanged)
            );

        private static void propChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

}
