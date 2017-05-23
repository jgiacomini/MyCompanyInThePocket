using System;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
	public class SettingView : UIView
	{
		public SettingView()
		{
			BackgroundColor = ApplicationColors.CellBackgroundColor;

			Label = new UILabel();
			Label.TextColor = ApplicationColors.ForegroundContentCellColor;
			Label.Font = UIFont.FromName(ApplicationStyle.ContentFontName, ApplicationStyle.TitleCellFontSize);

			this.AddSubviews(Label);

			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			nfloat hmargin = 15;

			this.AddConstraints(
				Label.WithSameCenterY(this),
				Label.AtLeftOf(this, hmargin));
		}

		public UILabel Label { get; set; }
	}
}
