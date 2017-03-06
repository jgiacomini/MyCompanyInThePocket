using System;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MyCompanyInThePocket.Core;
using MvvmCross.Binding.iOS.Views.Gestures;

namespace MyCompanyInThePocket.iOS
{
	public class UseFullLinkCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("UseFullLinkCell");

		protected UseFullLinkCell(IntPtr handle)
			: base(handle)
		{
			UseFullLinkView = new UseFullLinkView();
			Add(UseFullLinkView);
			this.DelayBind(OnCreateBinding);
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(UseFullLinkView.WithSameWidth(this), UseFullLinkView.WithSameHeight(this));
		}

		void OnCreateBinding()
		{
			var bindingSet = this.CreateBindingSet<UseFullLinkCell, UseFullLinkViewModel>();
			bindingSet.Bind(UseFullLinkView.Name).For(v => v.Text).To(vm => vm.Name);
			bindingSet.Bind(UseFullLinkView.Tap()).For(v => v.Command).To(vm => vm.TapCommand);
			bindingSet.Apply();
		}

		public UseFullLinkView UseFullLinkView { get; set; }
	}
}
