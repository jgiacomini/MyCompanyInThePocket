using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class MeetingView : UIView
	{
		public MeetingView()
		{
			BackgroundColor = ApplicationColors.CellBackgroundColor;
			TopSeparatorView = new UIView();
			TopSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

			LeftRectangleView = new UIView();
			LeftRectangleView.BackgroundColor = ApplicationColors.MainColor;
			Title = new UILabel();
			DateFormated = new UILabel();
			DateFormated.Font = DateFormated.Font.WithSize(12);
			Type = new UILabel();
			Type.Font = Type.Font.WithSize(12);
			BottomSeparatorView = new UIView();
			BottomSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

			AddSubviews(TopSeparatorView, LeftRectangleView, Title, DateFormated, Type,BottomSeparatorView);
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			nfloat vmargin = 5;
			nfloat hmargin = 15;

			nfloat separatorHeight = 1;
			this.AddConstraints(
				TopSeparatorView.WithSameWidth(this),
				TopSeparatorView.Height().EqualTo(separatorHeight),
				TopSeparatorView.AtTopOf(this),
				LeftRectangleView.AtTopOf(this, vmargin),
				LeftRectangleView.WithSameHeight(this).Minus(2 * vmargin),
				LeftRectangleView.Width().EqualTo(4),
				LeftRectangleView.AtLeftOf(this, hmargin),
				Title.AtTopOf(this, vmargin),
				Title.ToRightOf(LeftRectangleView, hmargin),
				DateFormated.AtBottomOf(this, vmargin),
				DateFormated.ToRightOf(LeftRectangleView, hmargin),
				Type.AtBottomOf(this, vmargin),
				Type.AtRightOf(this, hmargin),
				BottomSeparatorView.WithSameWidth(this),
				BottomSeparatorView.Height().EqualTo(separatorHeight),
				BottomSeparatorView.AtBottomOf(this));
		}

		public UILabel Title
		{
			get;
			set;
		}

		public UILabel DateFormated
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

		public UIView TopSeparatorView
		{
			get;
			set;
		}
		public UIView BottomSeparatorView
		{
			get;
			set;
		}
	}
}
