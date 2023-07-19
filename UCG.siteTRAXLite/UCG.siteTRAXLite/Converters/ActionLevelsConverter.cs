using System.Globalization;

namespace UCG.siteTRAXLite.Converters
{
    public class ActionLevelsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int levels)
            {
                return new string(' ', levels);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
