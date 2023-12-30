using System;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Data;
using FamilyFeud.MVVM.Enums;
using FamilyFeud.MVVM.Model;
using FamilyFeud.MVVM.ViewModel;

namespace FamilyFeud.MVVM.Converters
{
    public class EnumToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (value != null && (value is MainWindowViewModel.YesOrNo || value is Voice || value is Language))
            {
                return value.ToString();
            }

            return "Error";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}