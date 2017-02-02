using System;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using MyCompanyInThePocket.Core.ViewModels;
using TVST.Droid.UI.Views.Fragments;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace MyCompanyInThePocket.Droid.Views.Startup
{
    [Activity(Label = "View for StartupViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
    public class StartupView : AppCompatActivity
    {
        internal class LoginWebViewClient : WebViewClient
        {
            private readonly StartupViewModel _viewModel;

            public LoginWebViewClient(StartupViewModel viewModel)
            {
                _viewModel = viewModel;
            }

            public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
            {
                return false;
            }

            public override void OnPageStarted(WebView view, string url, Bitmap favicon)
            {
                base.OnPageStarted(view, url, favicon);
                var callbackUri = "http://jonathanantoine.com";


                if (url != null && url.IndexOf(callbackUri, StringComparison.OrdinalIgnoreCase) != -1)
                {

                    var indexOf = url.IndexOf("code=", StringComparison.Ordinal);
                    if (indexOf != -1)
                    {
                        var code = url.Substring(indexOf + "code=".Length).Split('&')[0];
                        if (!string.IsNullOrEmpty(code))
                        {
                            _viewModel.AuthentDoneAsync(code);
                        }
                    }
                }
            }
        }

        private WebView _startupWebView;
        private LinearLayout _startup_CircleIndicatorParent;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);

            SetContentView(Resource.Layout.StartupView);

            _startupWebView = FindViewById<WebView>(Resource.Id.Startup_WebView);
            var webViewClient = new LoginWebViewClient((StartupViewModel)ViewModel);

            _startupWebView.SetWebViewClient(webViewClient);
            _startupWebView.Settings.JavaScriptEnabled = true;

            var titleViewTypeface = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "fonts/Dosis-Light.ttf");

            var button = FindViewById<Button>(Resource.Id.Startup_RegisterButton);
            button.Click += Button_Click;
            button.Typeface = titleViewTypeface;

            button = FindViewById<Button>(Resource.Id.Startup_SignInButton);
            button.Click += Button_Click;
            button.Typeface = titleViewTypeface;

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

        public override void OnBackPressed()
        {
            if (_startupWebView.Visibility == ViewStates.Visible)
            {
                _startupWebView.Visibility = ViewStates.Gone;
            }
            else
            {
                base.OnBackPressed();
            }
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            var authUrl = "https://trakt.tv/oauth/authorize?client_id=f1a38fce7f2123c9a29e0fbac4db8e86ff1d0260484ce2e955ef4927167ae079&redirect_uri=http%3A%2F%2Fjonathanantoine.com&response_type=code";

            _startupWebView.Visibility = ViewStates.Visible;
            _startupWebView.LoadUrl(authUrl);
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
                        Title = tvshowtracker.i18n.Resources.Startup_FlipViewItem4_Title_Text,
                        Description = tvshowtracker.i18n.Resources.Startup_FlipViewItem4_Content_Text,
                        Drawable = Resource.Drawable.LoginImage4
                    };
                case 2:
                    return new StartupFeaturePanelFragment
                    {
                        Title = tvshowtracker.i18n.Resources.Startup_FlipViewItem3_Title_Text,
                        Description = tvshowtracker.i18n.Resources.Startup_FlipViewItem3_Content_Text,
                        Drawable = Resource.Drawable.LoginImage3
                    };
                case 1:
                    return new StartupFeaturePanelFragment
                    {
                        Title = tvshowtracker.i18n.Resources.Startup_FlipViewItem2_Title_Text,
                        Description = tvshowtracker.i18n.Resources.Startup_FlipViewItem2_Content_Text,
                        Drawable = Resource.Drawable.LoginImage2
                    };
                default:
                case 0:
                    return new StartupFeaturePanelFragment
                    {
                        Title = tvshowtracker.i18n.Resources.Startup_FlipViewItem1_Title_Text,
                        Description = tvshowtracker.i18n.Resources.Startup_FlipViewItem1_Content_Text,
                        Drawable = Resource.Drawable.LoginImage1
                    };
            }
        }
    }
}
