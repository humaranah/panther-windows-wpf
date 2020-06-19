using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TagLib;

namespace Panther.Windows.Views.Converters
{
    public class TagLibPictureToBitmapSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IPicture picture))
            {
                return null;
            }

            var image = new BitmapImage();

            using (var stream = new MemoryStream(picture.Data.Data))
            {
                stream.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = stream;
                image.EndInit();
            }

            image.Freeze();
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is BitmapSource source))
            {
                return null;
            }

            var encoder = new JpegBitmapEncoder
            {
                QualityLevel = 100
            };

            using var stream = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(source));
            encoder.Save(stream);
            var bytes = stream.ToArray();
            return new Picture(new ByteVector(bytes, bytes.Length));
        }
    }
}
