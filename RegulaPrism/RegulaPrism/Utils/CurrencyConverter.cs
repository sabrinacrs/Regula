using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Utils
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return float.Parse(value.ToString()); //.ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueFromString = value.ToString();//Regex.Replace(value.ToString(), @"\D", "");

            if (valueFromString.Length <= 0)
                return 0;

            decimal valueLong;
            if (!decimal.TryParse(valueFromString, out valueLong))
                return 0;

            if (valueLong < 0)
                return 0;

            return valueLong; // / 100;
        }
    }
}
