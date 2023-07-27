using System.Globalization;

namespace UCG.siteTRAXLite.Converters
{
    public class LevelToDotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int levels)
            {
                var result = "";
                for(var i  = 0; i < levels; i++)
                {
                    result += "\u00B7";
                }

                return result;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
