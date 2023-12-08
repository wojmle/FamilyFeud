using System;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Data;
using FamilyFeud.MVVM.Model;

namespace FamilyFeud.MVVM.Converters
{
    public class BoolToPoints : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (value != null && value[0] is bool visible && value[1] is int points)
            {
                if (visible)
                {
                    return points.ToString();
                }
                else
                {
                    return "...";
                }
            }
            return "...";
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
