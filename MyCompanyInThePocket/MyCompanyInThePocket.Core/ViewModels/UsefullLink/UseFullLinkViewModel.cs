using MvvmCross.Core.ViewModels;
using MyCompanyInThePocket.Core.Models;
using Plugin.Share;

namespace MyCompanyInThePocket.Core
{
	public class UseFullLinkViewModel
	{
		private readonly UseFullLink _useFullLink;
		private readonly MvxCommand _tapCommand;

		public UseFullLinkViewModel(UseFullLink useFullLink)
		{
			_useFullLink = useFullLink;
			_tapCommand = new MvxCommand(() =>
			{
				CrossShare.Current.OpenBrowser(Link);
			});
		}

		public string Link
		{
			get
			{
				return _useFullLink.Link;
			}
		}

		public string Name
		{
			get
			{
				return _useFullLink.Name;
			}
		}

		public byte[] Icon
		{
			get
			{
				return _useFullLink.Icon;
			}
		}

		public MvxCommand TapCommand
		{
			get
			{
				return _tapCommand;
			}
		}
	}
}
