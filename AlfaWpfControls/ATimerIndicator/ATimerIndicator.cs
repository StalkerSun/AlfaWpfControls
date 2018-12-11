using AlfaWpfControls.AContolTemplateUpdateEvent;
using AlfaWpfControls.types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AlfaWpfControls.ATimerIndicator
{
    public class ATimerIndicator : Control
    {

        #region local variable

        private DispatcherTimer _timer;

        private PathFigure _pathFigure;

        private ArcSegment _arcSegment;

        private Path _pathRoot;

        private AControlTemplateUpdateEvent _control;

        private int _countMillisecTimer = 20;





        #endregion


        #region Dep Property local

        private static readonly DependencyPropertyKey ArrayStateSegmentsPropertyKey
      = DependencyProperty.RegisterReadOnly("ArrayStateSegments", typeof(ActiveSegment[]), typeof(ATimerIndicator),
          new FrameworkPropertyMetadata(new ActiveSegment[18],
              FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty ArrayStateSegmentsProperty
            = ArrayStateSegmentsPropertyKey.DependencyProperty;

        public ActiveSegment[] ArrayStateSegments
        {
            get { return ( ActiveSegment[] ) GetValue(ArrayStateSegmentsProperty); }
            protected set { SetValue(ArrayStateSegmentsPropertyKey, value); }
        }



        //public ActiveSegment[] ArrayStateSegments
        //{
        //    get { return ( ActiveSegment[] ) GetValue(ArrayStateSegmentsProperty); }
        //    set { SetValue(ArrayStateSegmentsProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ArrayStateSegments.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ArrayStateSegmentsProperty =
        //    DependencyProperty.Register("ArrayStateSegments", typeof(ActiveSegment[]), typeof(ATimerIndicator), new PropertyMetadata(new ActiveSegment[18]));









        #region Colors

        private static readonly DependencyPropertyKey ActiveBrushPropertyKey
      = DependencyProperty.RegisterReadOnly("ActiveBrush", typeof(Brush), typeof(ATimerIndicator),
          new FrameworkPropertyMetadata(new SolidColorBrush(),
              FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty ActiveBrushProperty
            = ActiveBrushPropertyKey.DependencyProperty;

        public Brush ActiveBrush
        {
            get { return ( Brush ) GetValue(ActiveBrushProperty); }
            protected set { SetValue(ActiveBrushPropertyKey, value); }
        }


        private static readonly DependencyPropertyKey InactiveBrushPropertyKey
      = DependencyProperty.RegisterReadOnly("InactiveBrush", typeof(Brush), typeof(ATimerIndicator),
          new FrameworkPropertyMetadata(new SolidColorBrush(),
              FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty InactiveBrushProperty
            = InactiveBrushPropertyKey.DependencyProperty;

        public Brush InactiveBrush
        {
            get { return ( Brush ) GetValue(InactiveBrushProperty); }
            protected set { SetValue(InactiveBrushPropertyKey, value); }
        }


        #endregion

        #region Current Timers Value

        private static readonly DependencyPropertyKey CurrentTimerValuePropertyKey
      = DependencyProperty.RegisterReadOnly("CurrentTimerValue", typeof(TimeSpan), typeof(ATimerIndicator),
          new FrameworkPropertyMetadata(new TimeSpan(0, 0, 0),
              FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty CurrentTimerValueProperty
            = CurrentTimerValuePropertyKey.DependencyProperty;

        public TimeSpan CurrentTimerValue
        {
            get { return ( TimeSpan ) GetValue(CurrentTimerValueProperty); }
            protected set { SetValue(CurrentTimerValuePropertyKey, value); }
        }

        #endregion

        #endregion

        #region Dep Property public

        #region Colors

        /// <summary>Цвет заполнения активных индикатров </summary>
        public Color IndicatorActiveColor
        {
            get { return ( Color ) GetValue(IndicatorActiveColorProperty); }
            set { SetValue(IndicatorActiveColorProperty, value); }
        }

        public static readonly DependencyProperty IndicatorActiveColorProperty =
            DependencyProperty.Register("IndicatorActiveColor", typeof(Color), typeof(ATimerIndicator), new PropertyMetadata(Colors.Blue, ChangeIndicatorActiveColor));

        private static void ChangeIndicatorActiveColor(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = ( ATimerIndicator ) d;

            control.ActiveBrush = new SolidColorBrush(( Color ) e.NewValue);

        }


        /// <summary>Цвет заполнения неактивных индикатров</summary>
        public Color IndicatorInactiveColor
        {
            get { return ( Color ) GetValue(IndicatorInactiveColorProperty); }
            set { SetValue(IndicatorInactiveColorProperty, value); }
        }

        public static readonly DependencyProperty IndicatorInactiveColorProperty =
            DependencyProperty.Register("IndicatorInactiveColor", typeof(Color), typeof(ATimerIndicator), new PropertyMetadata(Colors.Gray, ChangeIndicatorInactiveColor));

        private static void ChangeIndicatorInactiveColor(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = ( ATimerIndicator ) d;

            control.InactiveBrush = new SolidColorBrush(( Color ) e.NewValue);
        }

        #endregion

        #region TypeViewDataIndicator

        public TypeViewDataIndicator TypeViewDataIndicator
        {
            get { return ( TypeViewDataIndicator ) GetValue(TypeViewDataIndicatorProperty); }
            set { SetValue(TypeViewDataIndicatorProperty, value); }
        }

        public static readonly DependencyProperty TypeViewDataIndicatorProperty =
            DependencyProperty.Register("TypeViewDataIndicator", typeof(TypeViewDataIndicator), typeof(ATimerIndicator), new PropertyMetadata(TypeViewDataIndicator.Timer));

        #endregion

        #region TypeViewIndicator



        public EnumTypeViewIndicator TypeViewIndicator
        {
            get { return ( EnumTypeViewIndicator ) GetValue(TypeViewIndicatorProperty); }
            set { SetValue(TypeViewIndicatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TypeViewIndicator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeViewIndicatorProperty =
            DependencyProperty.Register("TypeViewIndicator", typeof(EnumTypeViewIndicator), typeof(ATimerIndicator), new PropertyMetadata(EnumTypeViewIndicator.Dotted, TypeViewIndicatorChanged));

        private static void TypeViewIndicatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }



        #endregion

        #region Times

        public TimeSpan Duration
        {
            get { return ( TimeSpan ) GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(ATimerIndicator), new PropertyMetadata(new TimeSpan(0, 0, 0), DurationPropChangeCallback), DutationValidate);

        private static bool DutationValidate(object value)
        {
            var val = ( TimeSpan ) value;

            return val.TotalMinutes <= 99;
        }

        private static void DurationPropChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = ( ATimerIndicator ) d;

            if (control._timer != null)
            {
                control._timer.Stop();
            }

            control.CurrentTimerValue = ( TimeSpan ) e.NewValue;
        }
        #endregion

        #region PercentValue

        public double PercentValue
        {
            get { return ( double ) GetValue(PercentValueProperty); }
            set { SetValue(PercentValueProperty, value); }
        }

        public static readonly DependencyProperty PercentValueProperty =
            DependencyProperty.Register("PercentValue", typeof(double), typeof(ATimerIndicator), new PropertyMetadata(100.0), PercentValueValidate);

        private static bool PercentValueValidate(object value)
        {
            var val = ( double ) value;

            return ( ( val >= 0 ) && ( val <= 100 ) );
        }

        #endregion

        #region IndicatorStartLineCap

        public PenLineCap IndicatorStartLineCap
        {
            get { return ( PenLineCap ) GetValue(IndicatorStartLineCapProperty); }
            set { SetValue(IndicatorStartLineCapProperty, value); }
        }

        public static readonly DependencyProperty IndicatorStartLineCapProperty =
            DependencyProperty.Register("IndicatorStartLineCap", typeof(PenLineCap), typeof(ATimerIndicator), new PropertyMetadata(PenLineCap.Flat));

        #endregion

        #region IndicatorEndLineCap

        public PenLineCap IndicatorEndLineCap
        {
            get { return ( PenLineCap ) GetValue(IndicatorEndLineCapProperty); }
            set { SetValue(IndicatorEndLineCapProperty, value); }
        }

        public static readonly DependencyProperty IndicatorEndLineCapProperty =
            DependencyProperty.Register("IndicatorEndLineCap", typeof(PenLineCap), typeof(ATimerIndicator), new PropertyMetadata(PenLineCap.Round));



        #endregion

        #region CouterDawnCompliteEvent

        public static readonly RoutedEvent CouterDawnCompliteEvent = EventManager.RegisterRoutedEvent("CouterDawnComplite",
           RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ATimerIndicator));

        public event RoutedEventHandler CouterDawnComplite
        {
            add { AddHandler(CouterDawnCompliteEvent, value); }
            remove { RemoveHandler(CouterDawnCompliteEvent, value); }
        }

        private void RaiseCouterDawnCompliteEvent()
        {
            RaiseEvent(new RoutedEventArgs(CouterDawnCompliteEvent));
        }

        #endregion


        #endregion

        static ATimerIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ATimerIndicator), new FrameworkPropertyMetadata(typeof(ATimerIndicator)));

        }

        public override void OnApplyTemplate()
        {
            ActiveBrush = new SolidColorBrush(IndicatorActiveColor);

            InactiveBrush = new SolidColorBrush(IndicatorInactiveColor);

            _timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, _countMillisecTimer)
            };

            _timer.Tick += _timer_Tick;

            for (int i = 0; i < ArrayStateSegments.Length; i++)
            {
                ArrayStateSegments[i] = new ActiveSegment();
            }

            base.OnApplyTemplate();




            _control = ( AControlTemplateUpdateEvent ) ( Template.FindName("ViewIndicatorControl", this) );

            _control.UpdateTemplate += _control_UpdateTemplate;


        }

        private void _control_UpdateTemplate(object sender, RoutedEventArgs e)
        {
            var template = _control.Template;

            if (template == ( ControlTemplate ) this.FindResource("ATimerIndicator_FullLineIndicatorTemplate"))
            {
                _pathRoot = template.FindName("IndicatorPath", _control) as Path;

                _pathFigure = template.FindName("pathFigure", _control) as PathFigure;

                _arcSegment = template.FindName("arcSegment", _control) as ArcSegment;

                RenderArc(0);
            }

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == true)
            {
                UpdateViewIndicator(55);
            }
        }

        #region private methods

        private void _timer_Tick(object sender, EventArgs e)
        {
            var tmpVal = CurrentTimerValue - new TimeSpan(0, 0, 0, 0, _countMillisecTimer);

            if (tmpVal >= new TimeSpan(0, 0, 0))
            {
                CurrentTimerValue = tmpVal;

                var percent = ( ( int ) CurrentTimerValue.TotalMilliseconds * 100.0 ) / ( int ) Duration.TotalMilliseconds;

                UpdateViewIndicator(percent);
            }
            else
            {
                RaiseCouterDawnCompliteEvent();

                Stop();
            }
        }

        private void UpdateViewIndicator(double percent)
        {
            switch (TypeViewIndicator)
            {
                case EnumTypeViewIndicator.Dotted:

                    ArrayStateSegments = GetNewArray(ArrayStateSegments.Length, percent);

                    break;
                case EnumTypeViewIndicator.Full:

                    RenderArc(percent);

                    break;
                default:
                    break;
            }


        }

        private void RenderArc(double percent)
        {
            int strokeThickness = 24;

            if (_pathRoot == null || _control == null)
            {
                return;
            }

            var radius = ( 368 - 26 ) / 2 - strokeThickness;
            var angle = ( percent * 360 ) / 100;

            Point startPoint = new Point(radius, 0);
            Point endPoint = ComputeCartesianCoordinate(angle, radius);
            endPoint.X += radius;
            endPoint.Y += radius;

            _pathRoot.Width = radius * 2 + strokeThickness;
            _pathRoot.Height = radius * 2 + strokeThickness;
            _pathRoot.Margin = new Thickness(strokeThickness + 2, strokeThickness - 1, 1, -3);

            bool largeArc = angle > 180.0;

            Size outerArcSize = new Size(radius, radius);

            _pathFigure.StartPoint = startPoint;

            var flagX = startPoint.X >= endPoint.X - 0.00001 && startPoint.X <= endPoint.X + 0.00001;

            var flagY = startPoint.Y >= endPoint.Y - 0.00001 && startPoint.Y <= endPoint.Y + 0.00001;


            if (flagX && flagY)
            {
                endPoint.X -= 0.01;
            }

            _arcSegment.Point = endPoint;
            _arcSegment.Size = outerArcSize;
            _arcSegment.IsLargeArc = largeArc;

            if (percent == 0)
            {
                _pathRoot.Visibility = Visibility.Collapsed;
            }
            else
            {
                _pathRoot.Visibility = Visibility.Visible;
            }
        }

        private Point ComputeCartesianCoordinate(double angle, double radius)
        {
            // convert to radians
            double angleRad = ( Math.PI / 180.0 ) * ( angle - 90 );
            double x = radius * Math.Cos(angleRad);
            double y = radius * Math.Sin(angleRad);
            return new Point(x, y);
        }
        private ActiveSegment[] GetNewArray(int countElem, double percent)
        {
            if (countElem == 0)
            {
                throw new ArgumentException("Число эллементов не может быть равно 0");
            }

            ActiveSegment[] resArray = new ActiveSegment[countElem];

            for (int i = 0; i < countElem; i++)
            {
                resArray[i] = new ActiveSegment();
            }

            var valueOneElem = 100.0 / countElem;



            var countFullElem = percent / valueOneElem;

            for (int i = 0; i < ( int ) countFullElem; i++)
            {
                resArray[i] = new ActiveSegment() { Value = 1 };
            }

            if (countFullElem - ( int ) countFullElem != 0)
            {
                resArray[( int ) countFullElem].Value = countFullElem - ( int ) countFullElem;
            }

            return resArray;
        }

        #endregion


        #region public methods

        public void Stop()
        {
            if (_timer == null)
            {
                return;
            }

            _timer.Stop();
        }

        public void RunTimer()
        {
            if (_timer == null)
            {
                return;
            }

            if (Duration == TimeSpan.MinValue)
            {
                throw new ArgumentException("Для запуска необходимо установить начальное время отсчёта (Duration)");
            }

            _timer.Start();
        }

        public void Reset()
        {
            if (_timer == null)
            {
                return;
            }

            _timer.Stop();

            CurrentTimerValue = Duration;

        }

        #endregion

        ~ATimerIndicator()
        {
            _timer.Stop();
            _timer.Tick -= _timer_Tick;
            _control.UpdateTemplate -= _control_UpdateTemplate;
            _timer = null;
        }
    }

    public class BoolToBrushMultiValueConverter : IMultiValueConverter
    {

        private readonly Dictionary<string, Point[]> _paramGradientForSegment = new Dictionary<string, Point[]>
        {
            {"1", new Point[]{new Point(0.028, 0.363), new Point(0.968, 0.636)} },
            {"2", new Point[]{new Point(0.065, 0.175), new Point(0.939, 0.819)} },
            {"3", new Point[]{new Point(0.13, 0.104), new Point(0.863, 0.905)} },
            {"4", new Point[]{new Point(0.226, 0.058), new Point(0.763, 0.95)} },
            {"5", new Point[]{new Point(0.5, 0.0), new Point(0.5, 1)} },
            {"6", new Point[]{new Point(0.726, 0.036), new Point(0.251, 0.952)} },
            {"7", new Point[]{new Point(0.854, 0.083), new Point(0.121, 0.893)} },
            {"8", new Point[]{new Point(0.94, 0.166), new Point(0.056, 0.822)} },
            {"9", new Point[]{new Point(0.985, 0.35), new Point(0.013, 0.631)} },
            {"10", new Point[]{new Point(0.992, 0.653), new Point(0.005, 0.364)} },
            {"11", new Point[]{new Point(0.939, 0.821), new Point(0.065, 0.173)} },
            {"12", new Point[]{new Point(0.877, 0.905), new Point(0.123, 0.082)} },
            {"13", new Point[]{new Point(0.77, 0.962), new Point(0.254, 0.026)} },
            {"14", new Point[]{new Point(0.5, 1), new Point(0.5, 0)} },
            {"15", new Point[]{new Point(0.246, 0.973), new Point(0.734, 0.033)} },
            {"16", new Point[]{new Point(0.115, 0.909), new Point(0.891, 0.092)} },
            {"17", new Point[]{new Point(0.05, 0.803), new Point(0.944, 0.169)} },
            {"18", new Point[]{ new Point(0.03, 0.641), new Point(0.979, 0.360)} }
        };

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {


            if (!( values[0] is ActiveSegment ))
            {
                return Binding.DoNothing;
            }

            var valSegment = ( ActiveSegment ) values[0];

            if (!( values[1] is Color ))
            {
                return Binding.DoNothing;
            }

            var valColorOn = ( Color ) values[1];

            if (!( values[2] is Color ))
            {
                return Binding.DoNothing;
            }

            var valColorOff = ( Color ) values[2];




            if (valSegment.Value == 0)
            {
                return new SolidColorBrush(valColorOff);
            }

            if (valSegment.Value == 1)
            {
                return new SolidColorBrush(valColorOn);
            }

            var numberSegment = ( string ) parameter;

            var points = _paramGradientForSegment[numberSegment];

            LinearGradientBrush gb = new LinearGradientBrush
            {
                StartPoint = points[0],

                EndPoint = points[1]
            };
            gb.GradientStops.Add(new GradientStop(valColorOn, valSegment.Value));
            gb.GradientStops.Add(new GradientStop(valColorOff, valSegment.Value));

            return gb;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DurationToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!( value is TimeSpan ))
            {
                return Binding.DoNothing;
            }

            var val = ( TimeSpan ) value;

            return string.Format("{0:D2}:{1:D2}", ( int ) val.TotalMinutes, val.Seconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ActiveSegment
    {

        public double Value { get; set; }
    }

    public enum TypeViewDataIndicator
    {
        Timer,
        Percent
    }

    //[ValueConversion(typeof(object), typeof(object))]
    //public class BoolToBrushConverter : DependencyObject, IValueConverter
    //{
    //    //public Color ColorOff { get; set; }

    //    //public Color ColorOn { get; set; }






    //    public Color ColorOff
    //    {
    //        get { return ( Color ) GetValue(ColorOffProperty); }
    //        set { SetValue(ColorOffProperty, value); }
    //    }

    //    public static readonly DependencyProperty ColorOffProperty =
    //        DependencyProperty.Register("ColorOff", typeof(Color), typeof(BoolToBrushConverter));

    //    public Color ColorOn
    //    {
    //        get { return ( Color ) GetValue(ColorOnProperty); }
    //        set { SetValue(ColorOnProperty, value); }
    //    }

    //    public static readonly DependencyProperty ColorOnProperty =
    //        DependencyProperty.Register("ColorOn", typeof(Color), typeof(BoolToBrushConverter));

    //    private readonly Dictionary<string, Point[]> _paramGradientForSegment = new Dictionary<string, Point[]>
    //    {
    //        {"1", new Point[]{new Point(0.028, 0.363), new Point(0.968, 0.636)} },
    //        {"2", new Point[]{new Point(0.065, 0.175), new Point(0.939, 0.819)} },
    //        {"3", new Point[]{new Point(0.13, 0.104), new Point(0.863, 0.905)} },
    //        {"4", new Point[]{new Point(0.226, 0.058), new Point(0.763, 0.95)} },
    //        {"5", new Point[]{new Point(0.5, 0.0), new Point(0.5, 1)} },
    //        {"6", new Point[]{new Point(0.726, 0.036), new Point(0.251, 0.952)} },
    //        {"7", new Point[]{new Point(0.854, 0.083), new Point(0.121, 0.893)} },
    //        {"8", new Point[]{new Point(0.94, 0.166), new Point(0.056, 0.822)} },
    //        {"9", new Point[]{new Point(0.985, 0.35), new Point(0.013, 0.631)} },
    //        {"10", new Point[]{new Point(0.992, 0.653), new Point(0.005, 0.364)} },
    //        {"11", new Point[]{new Point(0.939, 0.821), new Point(0.065, 0.173)} },
    //        {"12", new Point[]{new Point(0.877, 0.905), new Point(0.123, 0.082)} },
    //        {"13", new Point[]{new Point(0.77, 0.962), new Point(0.254, 0.026)} },
    //        {"14", new Point[]{new Point(0.5, 1), new Point(0.5, 0)} },
    //        {"15", new Point[]{new Point(0.246, 0.973), new Point(0.734, 0.033)} },
    //        {"16", new Point[]{new Point(0.115, 0.909), new Point(0.891, 0.092)} },
    //        {"17", new Point[]{new Point(0.05, 0.803), new Point(0.944, 0.169)} },
    //        {"18", new Point[]{ new Point(0.03, 0.641), new Point(0.979, 0.360)} }
    //    };


    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        //if (!(value is bool)) return Binding.DoNothing;

    //        //var val = (bool)value;


    //        if (!( value is ActiveSegment ))
    //        {
    //            return Binding.DoNothing;
    //        }

    //        var val = ( ActiveSegment ) value;

    //        if (val.Value == 0)
    //        {
    //            return new SolidColorBrush(ColorOff);
    //        }

    //        if (val.Value == 1)
    //        {
    //            return new SolidColorBrush(ColorOn);
    //        }

    //        var numberSegment = ( string ) parameter;

    //        var points = _paramGradientForSegment[numberSegment];

    //        LinearGradientBrush gb = new LinearGradientBrush
    //        {
    //            StartPoint = points[0],

    //            EndPoint = points[1]
    //        };
    //        gb.GradientStops.Add(new GradientStop(ColorOn, val.Value));
    //        gb.GradientStops.Add(new GradientStop(ColorOff, val.Value));

    //        return gb;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }


    //}



}
