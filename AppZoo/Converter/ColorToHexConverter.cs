using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace AppZoo.Converter
{
    public class ColorToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            string hexString = ((string)value).Replace("#", "");
            byte a = byte.Parse(hexString.Substring(0, 2), NumberStyles.HexNumber);
            byte r = byte.Parse(hexString.Substring(2, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hexString.Substring(4, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hexString.Substring(6, 2), NumberStyles.HexNumber);

            Color color = Color.FromArgb(a, r, g, b);
            PropertyInfo propColor = GetPropertyInfoFromColor(color);

            return propColor;    
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            PropertyInfo propColor = (PropertyInfo)value;
            var corHex = ((Color)propColor.GetValue(null, null)).ToString();

            return corHex;
        }

        private PropertyInfo GetPropertyInfoFromColor(Color color)
        {
            Type ColorType = typeof(Windows.UI.Colors);
            PropertyInfo[] arrPiColors = ColorType.GetProperties(BindingFlags.Public | BindingFlags.Static);

            return arrPiColors.Where(t => (Color)t.GetValue(null, null) == color).FirstOrDefault();
        }
    }
}
