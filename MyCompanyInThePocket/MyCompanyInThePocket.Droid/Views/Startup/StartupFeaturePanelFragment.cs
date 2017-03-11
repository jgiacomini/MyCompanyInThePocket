using Android.OS;
using Android.Views;
using Android.Widget;
using MyCompanyInThePocket.Droid;
using Square.Picasso;

namespace MyCompanyInThePocket.Droid.Views.Startup
{
    public class StartupFeaturePanelFragment : Android.Support.V4.App.Fragment
    {
        private TextView _descriptionView;
        private TextView _titleView;
        private ImageView _imageView;

        public string Title { get; set; }
        public string Description { get; set; }
        public int Drawable { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var toReturn = GetLayoutInflater(savedInstanceState).Inflate(Resource.Layout.Startup_Fragment_FeaturePanel, null);

            _titleView = toReturn.FindViewById<TextView>(Resource.Id.Startup_FeatureTitle);
            _descriptionView = toReturn.FindViewById<TextView>(Resource.Id.Startup_FeatureDescription);
            _imageView = toReturn.FindViewById<ImageView>(Resource.Id.Startup_FeatureImage);

            return toReturn;
        }

        public override void OnResume()
        {
            _titleView.Text = Title;
            _descriptionView.Text = Description;
            Picasso.With(Context).Load(Drawable).Into(_imageView);
            base.OnResume();
        }
    }
}