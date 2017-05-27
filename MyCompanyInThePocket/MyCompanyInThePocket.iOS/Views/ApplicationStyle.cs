using System;
using System.Diagnostics;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public static class ApplicationStyle
	{
		public static string TitleFontName = "RobotoSlab-Regular";
		public static string TitleLightFontName = "RobotoSlab-Light";
		public static string TitleBoldFontName = "RobotoSlab-Bold";
		public static string TitleThinFontName = "RobotoSlab-Thin";

		public static nfloat TitleCellFontSize = 16;
		public static nfloat TitleCellContentFontSize = 12;
		public static nfloat TitleSectionCellFontSize = 12;
		public static nfloat TitleCellSubContentFontSize = 8;
		public static nfloat SettingButtonFontSize = 15;


		public static string ContentFontName = "Raleway-Regular";
		public static string ContentItalicFontName = "Raleway-Italic";
		public static string ContentLightFontName = "Raleway-Light";
		public static string ContentThinFontName = "Raleway-Thin";
		public static string ContentBoldFontName = "Raleway-Bold";

		public static string ToastFontName = "Raleway-Regular";
		public static nfloat ToastFontSize = 10;

		public static void DisplayFontsName()
		{
			foreach (var item in UIFont.FamilyNames)
			{
				Console.WriteLine(item);
				Debug.WriteLine(item);
				DisplayFontNamesForFamily(item);
			}
		}

		public static void DisplayFontNamesForFamily(string fontName)
		{ 
			foreach (var item in   UIFont.FontNamesForFamilyName(fontName))
			{
				Console.WriteLine(item);
        		Debug.WriteLine(item);
			}
		}

	}
}
