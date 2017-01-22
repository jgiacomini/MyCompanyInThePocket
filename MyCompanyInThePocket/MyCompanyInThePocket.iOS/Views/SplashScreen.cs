using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;

namespace MyCompanyInThePocket.iOS.Views
{
    public partial class SplashScreenView : MvxViewController
    {
        public SplashScreenView() : base("SplashScreen", null)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            RestorationIdentifier = "ViewControllerRestorationId";
            RestorationClass = new ObjCRuntime.Class(typeof(SplashScreenView));
        }
    }
}
