using System;
using Foundation;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class MeetingCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("MeetingCell");

        public MeetingCell(IntPtr handle)
		: base(handle)
		{
            Initialize();
		}

        public MeetingCell()
            : base(UITableViewCellStyle.Default, Key)
        {
            Initialize();
        }


        void Initialize()
        {
			MeetingView = new MeetingView();
			Add(MeetingView);
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(MeetingView.FullHeightOf(this, 3));
			this.AddConstraints(MeetingView.WithSameWidth(this));
        }

        public void OnApplyBinding(MeetingViewModel vm)
        {
            MeetingView.Title.Text = vm.Title;
            MeetingView.DurationFormated.Text = vm.DurationFormated;
            MeetingView.Type.Text = vm.Type;
        }

		public MeetingView MeetingView
		{
			get;
			private set;
		}
	}
}
