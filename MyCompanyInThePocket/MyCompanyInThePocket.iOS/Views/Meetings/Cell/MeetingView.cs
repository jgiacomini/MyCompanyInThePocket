using System;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class MeetingView : UIView
	{
		public MeetingView()
		{
			BackgroundColor = ApplicationColors.CellBackgroundColor;

			LeftRectangleView = new UIView();
			LeftRectangleView.BackgroundColor = ApplicationColors.MainColor;

			Title = new UILabel();
			Title.TextColor = ApplicationColors.ForegroundHeaderCellColor;
			Title.Font = UIFont.FromName(ApplicationStyle.ContentFontName, ApplicationStyle.TitleCellFontSize);
            Title.LineBreakMode = UILineBreakMode.TailTruncation;

			DurationFormated = new UILabel();
			DurationFormated.Font = UIFont.FromName(ApplicationStyle.ContentFontName, ApplicationStyle.TitleCellContentFontSize);
			DurationFormated.TextColor = ApplicationColors.ForegroundContentCellColor;

			Type = new UILabel();
			Type.Font = UIFont.FromName(ApplicationStyle.ContentLightFontName, ApplicationStyle.TitleCellSubContentFontSize);
			Type.Layer.BorderColor = ApplicationColors.MainColor.CGColor;
			Type.Layer.BorderWidth = 1f;
			Type.Layer.CornerRadius = 5;
			Type.ClipsToBounds = true;
			Type.Layer.MasksToBounds = true;

			Type.TextColor = ApplicationColors.ForegroundContentCellColor;
			Type.TextAlignment = UITextAlignment.Center;

			this.AddSubviews(LeftRectangleView,
			            Title, 
			            DurationFormated, 
			            Type);
			
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			nfloat vmargin = 5;
			nfloat hmargin = 15;

			this.AddConstraints(
				LeftRectangleView.AtTopOf(this, vmargin),
				LeftRectangleView.WithSameHeight(this).Minus(2 * vmargin),
				LeftRectangleView.Width().EqualTo(4),
				LeftRectangleView.AtLeftOf(this, hmargin),
				Title.AtTopOf(this, vmargin),
				Title.ToRightOf(LeftRectangleView, hmargin),
                Title.WithSameWidth(this).Minus(2* hmargin + 4),
                DurationFormated.AtBottomOf(this, vmargin),
				DurationFormated.ToRightOf(LeftRectangleView, hmargin),
                DurationFormated.AtRightOf(Type,vmargin),
                Type.AtBottomOf(this, vmargin),
				Type.AtRightOf(this, hmargin),
				Type.Width().EqualTo(60));
		}

		public UILabel Title
		{
			get;
			set;
		}

		public UILabel DurationFormated
		{
			get;
			set;
		}

		public UILabel Type
		{
			get;
			set;
		}

		public UIView LeftRectangleView
		{
			get;
			set;
		}
	}
}
