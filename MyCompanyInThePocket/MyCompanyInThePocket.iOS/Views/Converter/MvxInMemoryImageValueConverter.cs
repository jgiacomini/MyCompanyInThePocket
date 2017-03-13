using System;
using Foundation;
using MvvmCross.Platform.Converters;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
[	Preserve(AllMembers = true)]
	public class MvxInMemoryImageValueConverter : MvxValueConverter<byte[], UIImage>
	{
		protected override UIImage Convert(byte[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return null;

			var imageData = NSData.FromArray(value);
			return UIImage.LoadFromData(imageData);
		}
	}
}
