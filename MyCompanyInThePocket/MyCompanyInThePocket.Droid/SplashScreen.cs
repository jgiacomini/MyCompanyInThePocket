using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MyCompanyInThePocket.Droid
{
    [Activity(
        Label = "My Company In The Pocket"
        , MainLauncher = true
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            SetContentView(Resource.Layout.SplashScreen);
            base.OnCreate(savedInstanceState, persistentState);
        }
    }
}
