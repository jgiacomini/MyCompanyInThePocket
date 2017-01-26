using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace MyCompanyInThePocket.Droid.Views
{
    [Activity(Label = "")]
    public class MainScreenView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SplashScreenView);
        }
    }
}
