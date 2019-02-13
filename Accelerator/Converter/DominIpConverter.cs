using System;
using Windows.UI.Xaml.Data;

namespace Accelerator.Converter
{
    class DominIpConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            string domin = value.ToString().Split('/')[2];
            switch (domin)
            {
                case "static.iyuba.com":
                    domin =value.ToString().Replace(domin, "118.190.169.68");
                    break;
            }
            return domin;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
