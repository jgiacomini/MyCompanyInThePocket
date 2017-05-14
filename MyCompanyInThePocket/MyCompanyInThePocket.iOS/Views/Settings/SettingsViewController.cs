using System;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using UIKit;
using MyCompanyInThePocket.Core.Resources;
using GalaSoft.MvvmLight.Helpers;

namespace MyCompanyInThePocket.iOS.Views
{
    public class SettingsViewController : BaseViewController<SettingsViewModel>
    {
        private UIButton _logoutButton;
        private UIView _topSeparatorView;
        private UIView _bottomSeparatorView;
        private UIButton _goToStartupButton;

        public SettingsViewController()
            :base(false)
        {
            View.BackgroundColor = ApplicationColors.BackgroundColor;
        }

        public override void ViewDidLoad()
        {
            _topSeparatorView = new UIView();
            _topSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

            _logoutButton = UIButton.FromType(UIButtonType.System);
            _logoutButton.SetTitle(StringValues.Main_Settings_Logout, UIControlState.Normal);
            _logoutButton.SetTitleColor(UIColor.Red, UIControlState.Normal);
            _logoutButton.BackgroundColor = ApplicationColors.CellBackgroundColor;

            _goToStartupButton = UIButton.FromType(UIButtonType.System);
            _goToStartupButton.SetTitle(StringValues.Main_Settings_Intro, UIControlState.Normal);
            _goToStartupButton.SetTitleColor(ApplicationColors.ForegroundContentCellColor, UIControlState.Normal);
            _goToStartupButton.BackgroundColor = ApplicationColors.CellBackgroundColor;

            _bottomSeparatorView = new UIView();
            _bottomSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

            View.AddSubviews(_topSeparatorView, _logoutButton, _goToStartupButton, _bottomSeparatorView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            nfloat separatorHeight = 1;
            nfloat vmargin = 15;
            View.AddConstraints(
                _topSeparatorView.WithSameWidth(View),
                _topSeparatorView.Height().EqualTo(separatorHeight),
                _topSeparatorView.AtTopOf(View, vmargin),
                _logoutButton.WithSameWidth(View),
                _logoutButton.Below(_topSeparatorView),
                _goToStartupButton.WithSameWidth(View),
                _goToStartupButton.Below(_logoutButton),
                _bottomSeparatorView.WithSameWidth(View),
                _bottomSeparatorView.Height().EqualTo(separatorHeight),
                _bottomSeparatorView.Below(_goToStartupButton));


            _logoutButton.SetCommand(ViewModel.LogOutCommand);
            _goToStartupButton.SetCommand(ViewModel.ReviewIntroCommand);

            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}
