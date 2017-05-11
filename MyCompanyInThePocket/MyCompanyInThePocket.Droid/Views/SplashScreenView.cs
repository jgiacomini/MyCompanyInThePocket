using Android.App;
using Android.OS;
using MyCompanyInThePocket.Core.ViewModels;

namespace MyCompanyInThePocket.Droid.Views
{
    [Activity(Label = "")]
    public class MainScreenView : Activity
    {

        public MainViewModel ViewModel { get; set; }
  
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {

            ViewModel = new MainViewModel();
            base.OnCreate(savedInstanceState, persistentState);
        }
    }
}
