using System.Drawing;
using UIKit;
using Foundation;
using CoreGraphics;

namespace MyCompanyInThePocket.iOS.Views
{
    [Register("StartupPageFeatureView")]
    public class StartupPageFeatureView : UIView
    {
        public StartupPageFeatureView()
        {
            Initialize();
        }

        public StartupPageFeatureView(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        public UILabel HeadingLabel { get; private set; }
        public UIImageView ImageView { get; private set; }
        public UILabel SubHeadingLabel { get; private set; }

        void Initialize()
        {
            int subHeaderHeight = 160;
            int headerHeight = 40;
            const int buttonHeight = 50;
            var heightToUse = UIScreen.MainScreen.Bounds.Height - buttonHeight;

            var backgroundView = new UIView
            {
                BackgroundColor = UIColor.FromRGBA(0x00, 0x00, 0x00, 0xC1),
                Frame = new CGRect(0, heightToUse - subHeaderHeight - headerHeight,
                UIScreen.MainScreen.Bounds.Width, subHeaderHeight + headerHeight),
            };

            HeadingLabel = new UILabel
            {
                Font = UIFont.FromName(ApplicationStyle.TitleLightFontName, 40f),
                TextColor = UIColor.White,
                TextAlignment = UITextAlignment.Center,

                Frame = new CGRect(0, heightToUse + 20 - subHeaderHeight - headerHeight, UIScreen.MainScreen.Bounds.Width, headerHeight)
            };

            SubHeadingLabel = new UILabel
            {
                Font = UIFont.FromName(ApplicationStyle.ContentFontName, 22f),
                TextColor = UIColor.White,
                Lines = 0,
                TextAlignment = UITextAlignment.Center,
                Frame = new CGRect(0, heightToUse - subHeaderHeight, UIScreen.MainScreen.Bounds.Width, subHeaderHeight - 20)
            };

            ImageView = new UIImageView
            {
                ClipsToBounds = true,
                ContentMode = UIViewContentMode.ScaleAspectFill,
                Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, heightToUse)
            };

            AddSubview(ImageView);
            AddSubview(backgroundView);
            AddSubview(HeadingLabel);
            AddSubview(SubHeadingLabel);

            base.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, heightToUse);
        }
    }

    [Register("StartupPageFeatureViewController")]
    public class StartupPageFeatureViewController : UIViewController
    {
        private string _title;
        private string _subtitle;
        private string _image;

        public int PageIndex { get; set; }

        public StartupPageFeatureViewController(string title, string subtitle, int index)
        {
            _title = title;
            _subtitle = subtitle;
            _image = "LoginImage" + (index + 1);
            PageIndex = index;
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            var startupPageFeatureView = new StartupPageFeatureView();

            startupPageFeatureView.HeadingLabel.Text = _title;
            startupPageFeatureView.SubHeadingLabel.Text = _subtitle;
            startupPageFeatureView.ImageView.Image = UIImage.FromBundle(_image);

            base.View = startupPageFeatureView;
            base.ViewDidLoad();
        }
    }
}