﻿using System;
using UIKit;
using MyCompanyInThePocket.Core.Resources;

namespace MyCompanyInThePocket.iOS.Views
{
    public static class ColorStyleExtension
    {
        public static UIColor ToTextColor(this ColorStyle colorStyle)
        {
            switch(colorStyle)
			{
				case ColorStyle.Default:
					return UIColor.Clear.FromHex(0x5B5B5B);
                case ColorStyle.Primary:
                    return ApplicationColors.MainColor;
                case ColorStyle.Success:
					return UIColor.Clear.FromHex(0x3C763D);
                case ColorStyle.Info:
					return UIColor.Clear.FromHex(0x31708F);
				case ColorStyle.Warning:
					return UIColor.Clear.FromHex(0x8A6D3B);
                case ColorStyle.Danger:
					return UIColor.Clear.FromHex(0xA94442);
                default:
                    throw new NotImplementedException($"Unable to find the color corresponding to {colorStyle}");
            }
        }
    }
}