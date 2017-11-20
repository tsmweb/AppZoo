using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;
using Sullinger.ValidatableBase.Models;

namespace AppZoo.Converter
{
    public class ValidationCollectionToSingleStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is IEnumerable<IValidationMessage>))
            {
                throw new ArgumentException("View must provide the converter with a collection of IMessage objects.");
            }

            var collection = value as IEnumerable<IValidationMessage>;
            if (!collection.Any())
            {
                return string.Empty;
            }

            return collection.FirstOrDefault().Message;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
