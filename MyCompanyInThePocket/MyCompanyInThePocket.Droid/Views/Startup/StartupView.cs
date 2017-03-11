using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using MvvmCross.Droid.Support.V7.AppCompat;
using MyCompanyInThePocket.Core.ViewModels;

namespace MyCompanyInThePocket.Droid.Views.Startup
{
    [Activity(Label = "View for StartupViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
    public class StartupView : MvxAppCompatActivity<StartupViewModel>
    {
        private LinearLayout _startup_CircleIndicatorParent;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);

            SetContentView(Resource.Layout.StartupView);

            var button = FindViewById<Button>(Resource.Id.Startup_SignInButton);
            button.Click += Button_Click;

            var viewPager = FindViewById<ViewPager>(Resource.Id.Startup_ViewPager);
            viewPager.Adapter = new StartupPageAdapter(SupportFragmentManager);
            viewPager.OffscreenPageLimit = 5;
            viewPager.PageSelected += ViewPager_PageSelected;

            _startup_CircleIndicatorParent = FindViewById<LinearLayout>(Resource.Id.Startup_CircleIndicatorParent);
            SetCircleIndicatorsOpacity(new ViewPager.PageSelectedEventArgs(0));
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            SetCircleIndicatorsOpacity(e);
        }

        private void SetCircleIndicatorsOpacity(ViewPager.PageSelectedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                _startup_CircleIndicatorParent.GetChildAt(i).Alpha = (e.Position == i) ? 1 : 0.4f;
            }
        }


        private void Button_Click(object sender, System.EventArgs e)
        {
            // CONNECTION
        }
    }

    public class StartupPageAdapter : FragmentPagerAdapter
    {
        public StartupPageAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public StartupPageAdapter(FragmentManager fm) : base(fm) { }
        public override int Count => 4;
        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 3:
                    return new StartupFeaturePanelFragment
                    {
                        Title = Core.Resources.StringValues.Startup_FlipViewItem4_Title_Text,
                        Description = Core.Resources.StringValues.Startup_FlipViewItem4_Content_Text,
                        Drawable = Resource.Drawable.LoginImage4
                    };
                case 2:
                    return new StartupFeaturePanelFragment
                    {
                        Title = Core.Resources.StringValues.Startup_FlipViewItem3_Title_Text,
                        Description = Core.Resources.StringValues.Startup_FlipViewItem3_Content_Text,
                        Drawable = Resource.Drawable.LoginImage3
                    };
                case 1:
                    return new StartupFeaturePanelFragment
                    {
                        Title = Core.Resources.StringValues.Startup_FlipViewItem2_Title_Text,
                        Description = Core.Resources.StringValues.Startup_FlipViewItem2_Content_Text,
                        Drawable = Resource.Drawable.LoginImage2
                    };
                default:
                case 0:
                    return new StartupFeaturePanelFragment
                    {
                        Title = Core.Resources.StringValues.Startup_FlipViewItem1_Title_Text,
                        Description = Core.Resources.StringValues.Startup_FlipViewItem1_Content_Text,
                        Drawable = Resource.Drawable.LoginImage1
                    };
            }
        }
    }
}
