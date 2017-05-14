using System;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MyCompanyInThePocket.Core;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class UseFullLinkCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("UseFullLinkCell");
        private System.Windows.Input.ICommand _tapCommand;


		public UseFullLinkCell(IntPtr handle):
            base(handle)
		{
            Initialize();
		}

        public UseFullLinkCell()
            :base(UITableViewCellStyle.Default, Key)
		{
            Initialize();
		}

        void Initialize()
        {
			UseFullLinkView = new UseFullLinkView();
			Add(UseFullLinkView);
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(UseFullLinkView.FullHeightOf(this, 2));
			this.AddConstraints(UseFullLinkView.WithSameWidth(this));

            var tapGestureRecognizer = new UITapGestureRecognizer(OnTapped);
            this.AddGestureRecognizer(tapGestureRecognizer);
        }

        void OnTapped()
        {
            _tapCommand?.Execute(null);
        }

		public void OnApplyBinding(UseFullLinkViewModel vm)
		{
            UseFullLinkView.Name.Text = vm.Name;
            UseFullLinkView.Logo.Image = new InMemoryImageToUIImageConverter().Convert(vm.Icon, this.GetType(), null,null);
            _tapCommand = vm.TapCommand;
		}

		public UseFullLinkView UseFullLinkView { get; private set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _tapCommand = null;
        }

    }
}
 