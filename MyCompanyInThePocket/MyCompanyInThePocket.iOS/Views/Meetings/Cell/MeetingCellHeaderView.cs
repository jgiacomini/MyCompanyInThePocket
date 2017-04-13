using System;
using MyCompanyInThePocket.Core.ViewModels;
using UIKit;
using Cirrious.FluentLayouts.Touch;
namespace MyCompanyInThePocket.iOS
{
	public class MeetingCellHeaderView : UIView
	{
		public MeetingCellHeaderView(GroupedMeetingViewModel vm)
		{
			BackgroundColor = ApplicationColors.CellHeaderBackgroundColor;


			//var blur = UIBlurEffect.FromStyle(UIBlurEffectStyle.ExtraDark);
			//var blurView = new UIVisualEffectView(blur);
			//blurView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			//AddSubview(blurView);
			//blurView.Frame = Frame;

			Label = new UILabel();
			Label.Text = vm.DateFormated;
			Label.TextAlignment = UITextAlignment.Left;
			Label.TextColor = ApplicationColors.CellHeaderForegroundColor;
			Label.Font = Label.Font.WithSize(12);
			nfloat hmargin = 15;

			this.AddSubviews(Label);

			if (vm.IsNow)
			{
				//TODO : changer la couleur de fond et la couleur de font
			}

			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(
				Label.WithSameWidth(this).Minus(hmargin),
				Label.WithSameHeight(this),
				Label.WithSameCenterX(this),
				Label.WithSameCenterY(this)
			);
		}

		public UILabel Label
		{
			get;
			set;
		}


		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			Label?.Dispose();
		}
	}
}
