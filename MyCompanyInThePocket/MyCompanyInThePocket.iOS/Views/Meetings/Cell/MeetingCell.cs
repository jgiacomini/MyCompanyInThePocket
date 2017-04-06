using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
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
			this.AddConstraints(MeetingView.FullHeightOf(this, 3));
			this.AddConstraints(MeetingView.WithSameWidth(this));
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
