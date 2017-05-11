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
        }

		public void OnApplyBinding(UseFullLinkViewModel vm)
		{
            UseFullLinkView.Name.Text = vm.Name;
            UseFullLinkView.Logo.Image = new InMemoryImageToUIImageConverter().Convert(vm.Icon, this.GetType(), null,null);
            //bindingSet.Bind(UseFullLinkView.Tap()).For(v => v.Command).To(vm => vm.TapCommand);
		}

		public UseFullLinkView UseFullLinkView { get; private set; }

    }
}
 