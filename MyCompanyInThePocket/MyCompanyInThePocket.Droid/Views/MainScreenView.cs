using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace MyCompanyInThePocket.Droid.Views
{
    [Activity(Label = "View for SplashScreenView", NoHistory = true)]
    public class SplashScreenView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SplashScreenView);
        }
    }
}
