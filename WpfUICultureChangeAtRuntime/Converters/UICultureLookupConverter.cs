using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using WpfUICultureChangeAtRuntime.ViewModels;

namespace WpfUICultureChangeAtRuntime.Converters
{
    [ValueConversion(typeof(Dictionary<string, string>), typeof(string))]
    internal class UICultureLookupConverter : MarkupExtension, IValueConverter
    {
        private static UICultureLookupConverter _sharedConverter;

        static UICultureLookupConverter()
        {
            _sharedConverter = new UICultureLookupConverter();
        }

        public static Binding CreateBinding(string key) => new Binding("CurrentLanguageDictionnary")
        {
            Source = CommonViewModel.Current,
            Converter = _sharedConverter,
            ConverterParameter = key,
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = parameter as string;
            var languageDictionary = value as Dictionary<string, string>;

            if (languageDictionary != null && key != null)
                return languageDictionary.FirstOrDefault(loc => loc.Key == key).Value;
            Debug.WriteLine("The language dictionnary is empty");
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
