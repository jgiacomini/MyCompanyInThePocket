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
			Logo = new UIImageView();
			Logo.BackgroundColor = UIColor.Gray;
			Name = new UILabel();

			AddSubviews(Logo, Name);

			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			nfloat margin = 10;
			nfloat smallMargin = 2;
			this.AddConstraints(
				Logo.AtTopOf(this, margin),
				Logo.AtLeftOf(this, margin),
				Logo.Width().EqualTo(20),
				Logo.Height().EqualTo(20),
				Name.AtTopOf(this, margin),
				Name.ToRightOf(Logo, margin));
		}

		public UIImageView Logo { get; set; }

		public UILabel Name { get; set; }
	}
}
