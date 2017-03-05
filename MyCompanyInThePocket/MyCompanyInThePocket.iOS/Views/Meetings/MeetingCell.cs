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

	public class MeetingCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("MeetingCell");

		protected MeetingCell(IntPtr handle)
		: base(handle)
		{
			MeetingView = new MeetingView();
			Add(MeetingView);
			this.DelayBind(OnCreateBinding);
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(MeetingView.WithSameWidth(this),
						   MeetingView.WithSameHeight(this));
		}

		void OnCreateBinding()
		{
			var bindingSet = this.CreateBindingSet<MeetingCell, MeetingViewModel>();
			bindingSet.Bind(MeetingView.Title).For(v => v.Text).To(m => m.Title);
			bindingSet.Bind(MeetingView.DateFormated).For(v => v.Text).To(m => m.DateFormatted);
			bindingSet.Bind(MeetingView.Type).For(v => v.Text).To(m => m.Type);
			bindingSet.Apply();
		}

		public MeetingView MeetingView
		{
			get;
			private set;
		}


	}
}
