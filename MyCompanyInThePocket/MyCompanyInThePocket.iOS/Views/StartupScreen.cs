﻿using UIKit;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using System;

namespace MyCompanyInThePocket.iOS.Views
{
	public class StartupScreen : BaseScreen<StartupViewModel>
	{
		private UIButton _buttonNextPage;

		public StartupScreen()
		{
			NoHistory = true;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_buttonNextPage = UIButton.FromType(UIButtonType.System);
			_buttonNextPage.SetTitle("Lancez l'application", UIControlState.Normal);

			View.AddSubviews(_buttonNextPage);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			View.AddConstraints(
				_buttonNextPage.WithSameCenterY(View),
				_buttonNextPage.WithSameCenterX(View),
				_buttonNextPage.WithSameWidth(View)
			);

			var set = this.CreateBindingSet<StartupScreen, StartupViewModel>();
			set.Bind(_buttonNextPage).To(vm => vm.GoToNextPageCommand);
			set.Apply();
		}
	}
}
