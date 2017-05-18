using System;
using Foundation;
using UIKit;
using MyCompanyInThePocket.Core.Converter;
using System.Globalization;

namespace MyCompanyInThePocket.iOS
{
    [Preserve(AllMembers = true)]
    public class InMemoryImageToUIImageConverter : IValueConverter<byte[], UIImage>
    {
        public UIImage Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var imageData = NSData.FromArray(value);
            return UIImage.LoadFromData(imageData);
        }
    }
}
