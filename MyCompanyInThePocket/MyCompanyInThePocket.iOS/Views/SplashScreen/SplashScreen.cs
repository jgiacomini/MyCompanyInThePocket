using UIKit;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using System;

namespace MyCompanyInThePocket.iOS.Views
{
	public partial class SplashScreenView : BaseScreen<SplashScreenViewModel>, INoHistoryScreen
    {
		private UILabel _currentStateLabel;
		private UILabel _errorLabel;
		private UIActivityIndicatorView _activityIndicator;

        public SplashScreenView()
        {
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();
			_currentStateLabel = new UILabel();
			_currentStateLabel.TextAlignment = UITextAlignment.Center;

			_errorLabel = new UILabel();
			_errorLabel.TextAlignment = UITextAlignment.Center;

			_activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			_activityIndicator.StartAnimating();

			View.AddSubviews(_activityIndicator,_currentStateLabel, _errorLabel);

			nfloat margin = 10;
					 
			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			View.AddConstraints(
				_activityIndicator.WithSameCenterY(View),
				_activityIndicator.WithSameCenterX(View),
				_activityIndicator.WithSameWidth(View),

				_currentStateLabel.Below(_activityIndicator, margin),
				_currentStateLabel.WithSameWidth(View),

				_errorLabel.AtBottomOf(View, margin),
				_errorLabel.WithSameWidth(View)
			);


			var set = this.CreateBindingSet<SplashScreenView, SplashScreenViewModel>();
			set.Bind(_currentStateLabel).For(b => b.Text).To(x => x.CurrentState).OneWay();
			set.Bind(_errorLabel).For(b => b.Text).To(x => x.ErrorMessage).OneWay();
			set.Bind(_errorLabel).For(b => b.Hidden).To(x => x.HasError).OneWay();

			set.Apply();
			await ViewModel.InitializeAsync();
		}
    }
}
