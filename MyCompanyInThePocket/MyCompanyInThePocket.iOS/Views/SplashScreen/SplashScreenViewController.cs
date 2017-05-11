using UIKit;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using System;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;

namespace MyCompanyInThePocket.iOS.Views
{
    public class SplashScreenViewController : BaseViewController<SplashScreenViewModel>
    {
        private UILabel _currentStateLabel;
        private UILabel _errorLabel;
        private UIActivityIndicatorView _activityIndicator;

        public SplashScreenViewController(): 
        base(true)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _currentStateLabel = new UILabel();
            _currentStateLabel.TextAlignment = UITextAlignment.Center;

            _errorLabel = new UILabel();
            _errorLabel.TextAlignment = UITextAlignment.Center;

            _activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            _activityIndicator.StartAnimating();

            View.AddSubviews(_activityIndicator, _currentStateLabel, _errorLabel);

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

            ViewModel.InitializeAsync();
        }

        protected override void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs arg)
        {
            _currentStateLabel.Text = ViewModel.CurrentState;
            _errorLabel.Text = ViewModel.ErrorMessage;
            _errorLabel.Hidden = ViewModel.HasError;

		}
    }
}
