using System;
using UIKit;
namespace MyCompanyInThePocket.iOS
{
	public static class ApplicationColors
	{
		public static UIColor MainColor = UIColor.Clear.FromHex(0x007DB6);
		private static UIColor _mainLightColor;

        public static UIColor MainLightColor 
        {
            get
            {
                if (_mainLightColor == null)
                {
                    nfloat red;
                    nfloat green;
                    nfloat blue;
                    nfloat alpha;
                    MainColor.GetRGBA(out red, out green, out blue, out alpha);
                   _mainLightColor = UIColor.FromRGBA(red, green, blue, .6f);
                }

                return _mainLightColor;
			}
        }
        public static UIColor WindowBackgroundColor = UIColor.White;
		public static UIColor BackgroundColor = UIColor.Clear.FromHex(0xF9F9FB);
		public static UIColor SeparatorColor = UIColor.Clear.FromHex(0xEBEBEB);

		public static UIColor ToastBackgroundColor = UIColor.Clear.FromHex(0x333333);
		public static UIColor ToastForegroundColor = UIColor.White;

		public static UIColor ForegroundHeaderCellColor = UIColor.Black;
        public static UIColor ForegroundSettingCellColor = UIColor.Black;
		public static UIColor ForegroundContentCellColor = UIColor.Clear.FromHex(0x5B5B5B);
		public static UIColor CellHeaderBackgroundColor = UIColor.Clear.FromHex(0xF8F8F8);
		public static UIColor CellHeaderForegroundColor = UIColor.Clear.FromHex(0x8B8B8D);

		public static UIColor CellBackgroundColor = UIColor.White;
	}

}
