using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using MyCompanyInThePocket.Core.ViewModels;

namespace MyCompanyInThePocket.Droid.Views
{
    [Activity(Label = "")]
    public class MainScreenView : MvxActivity<SplashScreenViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ViewModel.InitializeAsync();
        }
    }
}
