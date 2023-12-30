using System;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using FamilyFeud.MVVM.Enums;
using FamilyFeud.MVVM.Model;
using FamilyFeud.MVVM.ViewModel;

namespace FamilyFeud.MVVM.Converters
{
    public class BoolToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (value != null && value is bool booleanValue)
            {
                if (booleanValue)
                {
                    return new SolidColorBrush(Color.FromArgb(6,100, 1, 1));
                }
                else
                {
                    return new SolidColorBrush(Color.FromArgb(6, 255, 255, 255));
                }
            }

            return new SolidColorBrush(Color.FromArgb(6, 255, 255, 255));
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}