using System;
using System.Globalization;
using System.Windows.Data;

namespace AlfaWpfControls.Convertors
{
    public class FormatConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                string formatterString = parameter.ToString();

                if (!string.IsNullOrEmpty(formatterString))
                {
                    return string.Format(culture, formatterString, value);
                }
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
