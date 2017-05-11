using UIKit;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using System;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;

namespace MyCompanyInThePocket.iOS.Views
{
    public class SplashScreenViewController : BaseViewController<SplashScreenViewModel>, INoHistoryScreen
    {
        private UILabel _currentStateLabel;
        private UILabel _errorLabel;
        private UIActivityIndicatorView _activityIndicator;

        public SplashScreenViewController()
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

            var bindings = new List<Binding>();

            bindings.Add(new Binding<bool, bool>(ViewModel, nameof(SplashScreenViewModel.CurrentState), _currentStateLabel, nameof(UILabel.Text), BindingMode.OneWay));
            bindings.Add(new Binding<bool, bool>(ViewModel, nameof(SplashScreenViewModel.ErrorMessage), _errorLabel, nameof(UILabel.Text), BindingMode.OneWay));
            bindings.Add(new Binding<bool, bool>(ViewModel, nameof(SplashScreenViewModel.HasError), _errorLabel, nameof(UILabel.Hidden), BindingMode.OneWay));

            ViewModel.InitializeAsync();
        }
    }
}
