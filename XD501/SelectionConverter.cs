using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XD501
{
    public class SelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch (value)
            {
                case "A":
                    return "Archivieren";
                case "V":
                    return "Vernichten";
                case "B":
                    return "Bewerten";
                default:
                    return value;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "Archivieren":
                    return "A";
                case "Vernichten":
                    return "V";
                case "Bewerten":
                    return "B";
                default:
                    return value;
            }
        }
    }
}
