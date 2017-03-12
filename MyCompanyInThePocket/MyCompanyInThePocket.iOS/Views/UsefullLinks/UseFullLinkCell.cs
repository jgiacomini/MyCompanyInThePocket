using System;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MyCompanyInThePocket.Core;
using MvvmCross.Binding.iOS.Views.Gestures;
using UIKit;

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
			this.AddConstraints(UseFullLinkView.FullHeightOf(this, 2));
			this.AddConstraints(UseFullLinkView.WithSameWidth(this));
		}

		void OnCreateBinding()
		{
			var bindingSet = this.CreateBindingSet<UseFullLinkCell, UseFullLinkViewModel>();
			bindingSet.Bind(UseFullLinkView.Name).For(v => v.Text).To(vm => vm.Name);

			bindingSet.Bind(UseFullLinkView.Logo).For(v => v.Image).To(vm => vm.Icon).WithConversion(new MvxInMemoryImageValueConverter());
			bindingSet.Bind(UseFullLinkView.Tap()).For(v => v.Command).To(vm => vm.TapCommand);
			bindingSet.Apply();
		}

		public UseFullLinkView UseFullLinkView { get; private set; }
	}
}
