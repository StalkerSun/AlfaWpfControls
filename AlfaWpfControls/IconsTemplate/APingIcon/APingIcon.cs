using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AlfaWpfControls.IconsTemplate.APingIcon
{
    public class APingIcon : Control
    {


        public Brush ActiveComputerBrush
        {
            get { return ( Brush ) GetValue(ActiveComputerBrushProperty); }
            set { SetValue(ActiveComputerBrushProperty, value); }
        }
        public static readonly DependencyProperty ActiveComputerBrushProperty =
            DependencyProperty.Register("ActiveComputerBrush", typeof(Brush), typeof(APingIcon), new PropertyMetadata(Brushes.Green));



        public Brush PassiveComputerBrush
        {
            get { return ( Brush ) GetValue(PassiveComputerBrushProperty); }
            set { SetValue(PassiveComputerBrushProperty, value); }
        }

        public static readonly DependencyProperty PassiveComputerBrushProperty =
            DependencyProperty.Register("PassiveComputerBrush", typeof(Brush), typeof(APingIcon), new PropertyMetadata(Brushes.Transparent));



        public Brush ActivePingRoundBrush
        {
            get { return ( Brush ) GetValue(ActivePingRoundBrushProperty); }
            set { SetValue(ActivePingRoundBrushProperty, value); }
        }

        public static readonly DependencyProperty ActivePingRoundBrushProperty =
            DependencyProperty.Register("ActivePingRoundBrush", typeof(Brush), typeof(APingIcon), new PropertyMetadata(Brushes.Green));



        public Brush PassivePingRoundBrush
        {
            get { return ( Brush ) GetValue(PassivePingRoundBrushProperty); }
            set { SetValue(PassivePingRoundBrushProperty, value); }
        }

        public static readonly DependencyProperty PassivePingRoundBrushProperty =
            DependencyProperty.Register("PassivePingRoundBrush", typeof(Brush), typeof(APingIcon), new PropertyMetadata(Brushes.White));



        public bool IsPlayAnimationPing
        {
            get { return ( bool ) GetValue(IsPlayAnimationPingProperty); }
            set { SetValue(IsPlayAnimationPingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPlayAnimationPing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPlayAnimationPingProperty =
            DependencyProperty.Register("IsPlayAnimationPing", typeof(bool), typeof(APingIcon), new PropertyMetadata(false));







        static APingIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(APingIcon), new FrameworkPropertyMetadata(typeof(APingIcon)));
        }


    }
}
