using System;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MyCompanyInThePocket.Core.ViewModels.Settings;

namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
	public class SettingCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("SettingCell");

		public SettingCell(IntPtr handle)
		: base(handle)
		{
			SettingView = new SettingView();
			AddSubview(SettingView);
			this.DelayBind(OnCreateBinding);
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(SettingView.FullHeightOf(this, 2));
			this.AddConstraints(SettingView.WithSameWidth(this));
		}

		void OnCreateBinding()
		{
			var bindingSet = this.CreateBindingSet<SettingCell, SettingViewModel>();
			bindingSet.Bind(SettingView.Label).For(v => v.Text).To(m => m.Label);
			bindingSet.Apply();
		}

		public SettingView SettingView { get; private set; }
	}
}
