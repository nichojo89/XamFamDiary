using System;
using Xamarin.Forms;
using System.Globalization;

namespace XamFamDiary.Converters
{
    public class ImageFileToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts photo file path to image source
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;
            return ImageSource.FromFile(path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}