using System.Globalization;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Converters
{
    public class IndexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var action = value as ActionItemEntity;
                if (action.Index % 2 == 0)
                {
                    return Color.FromArgb("#EDF0F6");
                }
            }

            return Color.FromArgb("#FFFFFF");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
