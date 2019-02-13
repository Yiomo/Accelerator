using System;
using Windows.UI.Xaml.Data;

namespace Accelerator.Converter
{
    public class TimeStampConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            int min = Convert.ToInt32(value) / 60;
            int sec = Convert.ToInt32(value) % 60;
            return string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
