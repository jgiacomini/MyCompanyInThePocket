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
			Title = new UILabel();
			DateFormated = new UILabel();
			Type = new UILabel();

			AddSubviews(Title, DateFormated, Type);

			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			nfloat margin = 5;
			this.AddConstraints(
				Title.AtTopOf(this, margin),
				Title.AtLeftOf(this, margin),
				Title.AtRightOf(this, margin),
				DateFormated.AtBottomOf(this, margin),
				DateFormated.AtLeftOf(this, margin),
				Type.AtBottomOf(this, margin),
				Type.AtRightOf(this, margin));
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
	}
}
