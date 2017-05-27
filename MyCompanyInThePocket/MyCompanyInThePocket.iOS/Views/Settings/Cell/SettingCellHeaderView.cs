using System;
using Cirrious.FluentLayouts.Touch;
using MyCompanyInThePocket.Core.ViewModels.Settings;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
	public class SettingCellHeaderView : UIView
	{
		public SettingCellHeaderView(GroupedSettingsViewModel vm)
		{
			BackgroundColor = ApplicationColors.CellHeaderBackgroundColor;

			Label = new UILabel();
			Label.Text = vm.Section;
			Label.TextAlignment = UITextAlignment.Left;
			Label.TextColor = ApplicationColors.CellHeaderForegroundColor;
            Label.Font = UIFont.FromName(ApplicationStyle.TitleFontName, ApplicationStyle.TitleSectionCellFontSize);
			nfloat hmargin = 15;
			nfloat vmargin = 5;


			this.AddSubviews(Label);

			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(
				Label.WithSameWidth(this).Minus(hmargin * 2),
                Label.AtLeftOf(this, hmargin),
                Label.AtBottomOf(this, vmargin)
			);
		}

		public UILabel Label { get; set; }

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			Label?.Dispose();
		}
	}
}
