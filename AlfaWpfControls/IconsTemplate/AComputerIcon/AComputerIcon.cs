using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AlfaWpfControls.IconsTemplate.AComputerIcon
{
    public class AComputerIcon : Control
    {
        public Brush MainColor
        {
            get { return ( Brush ) GetValue(MainColorProperty); }
            set { SetValue(MainColorProperty, value); }
        }

        public static readonly DependencyProperty MainColorProperty =
            DependencyProperty.Register("MainColor", typeof(Brush), typeof(AComputerIcon), new PropertyMetadata(new SolidColorBrush(( Color ) ColorConverter.ConvertFromString("#757575"))));



        public Brush ScreenColor
        {
            get { return ( Brush ) GetValue(ScreenColorProperty); }
            set { SetValue(ScreenColorProperty, value); }
        }
        public static readonly DependencyProperty ScreenColorProperty =
            DependencyProperty.Register("ScreenColor", typeof(Brush), typeof(AComputerIcon), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));




        static AComputerIcon()

        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AComputerIcon), new FrameworkPropertyMetadata(typeof(AComputerIcon)));
        }

    }
}
