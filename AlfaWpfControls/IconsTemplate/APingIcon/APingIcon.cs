using System;
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
            DependencyProperty.Register("ActiveComputerBrush", typeof(Brush), typeof(APingIcon), new PropertyMetadata(Brushes.Blue, ActiveComputerBrushChanged));

        private static void ActiveComputerBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = ( APingIcon ) d;

            obj.ActiveComputerColor = (Color)( ( Brush ) e.NewValue ).GetValue(SolidColorBrush.ColorProperty);
        }

        public Color ActiveComputerColor
        {
            get { return ( Color ) GetValue(ActiveComputerColorProperty); }
            set { SetValue(ActiveComputerColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActiveComputerColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveComputerColorProperty =
            DependencyProperty.Register("ActiveComputerColor", typeof(Color), typeof(APingIcon), new PropertyMetadata(Colors.Blue));


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
            DependencyProperty.Register("IsPlayAnimationPing", typeof(bool), typeof(APingIcon), new PropertyMetadata(false, IsPlayAnimationChangeCallback));

        private static void IsPlayAnimationChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        static APingIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(APingIcon), new FrameworkPropertyMetadata(typeof(APingIcon)));
        }

        public override void OnApplyTemplate()
        {
            //Template.Resources["ActiveColorPingRound"] = ActiveComputerColor;


            //var obj = ( Color ) this.Template.Resources["ActiveColorPingRound"];

            //obj = ActiveComputerColor;

            //var obj1 = ((Color)FindResource("ActiveColorPingRound"));
            base.OnApplyTemplate();

            ( Template.Resources["ActiveColorComp"] as SolidColorBrush ).Color = Colors.Yellow;
            Template.Resources["ActiveColorPingRound"] = ActiveComputerColor;

            //var a = Template.Resources["ActiveColorComp"] as SolidColorBrush;

            

            
            
            //var obj = (Color)this.Template.Resources["ActiveColorPingRound"];
            //(this.Template.Resources["ActiveColorPingRound"] as Color).A= ActiveComputerColor.A;

            

        }


    }
}
