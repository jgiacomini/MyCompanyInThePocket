using System;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class UseFullLinkView : UIView
	{
		public UseFullLinkView()
		{
			BackgroundColor = ApplicationColors.CellBackgroundColor;
			TopSeparatorView = new UIView();
			TopSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

			Logo = new UIImageView();
			Logo.BackgroundColor = ApplicationColors.MainColor;
			Name = new UILabel();

			BottomSeparatorView = new UIView();
			BottomSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

			AddSubviews(TopSeparatorView, Logo, Name, BottomSeparatorView);

			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

			nfloat separatorHeight = 1;
			nfloat vmargin = 5;
			nfloat hmargin = 15;
			nfloat imageSize = 35;

			this.AddConstraints(
				TopSeparatorView.WithSameWidth(this),
				TopSeparatorView.Height().EqualTo(separatorHeight),
				TopSeparatorView.AtTopOf(this),
				Logo.AtTopOf(this, vmargin),
				Logo.AtLeftOf(this, hmargin),
				Logo.Width().EqualTo(imageSize),
				Logo.Height().EqualTo(imageSize),
				Name.AtTopOf(this, vmargin),
				Name.ToRightOf(Logo, hmargin),
				BottomSeparatorView.WithSameWidth(this),
				BottomSeparatorView.Height().EqualTo(separatorHeight),
				BottomSeparatorView.AtBottomOf(this));
		}

		public UIImageView Logo { get; set; }

		public UILabel Name { get; set; }

		public UIView TopSeparatorView { get; set; }

		public UIView BottomSeparatorView { get; set; }
	}
}
