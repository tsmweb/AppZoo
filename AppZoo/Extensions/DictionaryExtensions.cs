namespace AppZoo.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Sullinger.ValidatableBase.Models;


    public static class DictionaryExtensions
    {
        public static Dictionary<string, ObservableCollection<IValidationMessage>> ConvertValidationMessagesToObservable(this Dictionary<string, IEnumerable<IValidationMessage>> messages)
        {
            if (messages == null)
            {
                return null;
            }

            var observableCollection = new Dictionary<string, ObservableCollection<IValidationMessage>>();
            foreach (KeyValuePair<string, IEnumerable<IValidationMessage>> pair in messages)
            {
                observableCollection.Add(pair.Key, new ObservableCollection<IValidationMessage>(pair.Value));
            }

            return observableCollection;
        }
    }
}
