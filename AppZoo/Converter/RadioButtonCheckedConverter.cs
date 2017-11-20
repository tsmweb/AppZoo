using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AppZoo.Converter
{
    public class RadioButtonCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value != null ? value.Equals(parameter) : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((bool)value) ? parameter : false;
        }
    }
}
