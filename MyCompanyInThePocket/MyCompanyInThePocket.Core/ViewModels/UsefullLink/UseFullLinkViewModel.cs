using GalaSoft.MvvmLight.Command;
using MyCompanyInThePocket.Core.Models;
using Plugin.Share;
using System.Windows.Input;

namespace MyCompanyInThePocket.Core
{
	public class UseFullLinkViewModel
	{
		private readonly UseFullLink _useFullLink;
		private readonly ICommand _tapCommand;

		public UseFullLinkViewModel(UseFullLink useFullLink)
		{
			_useFullLink = useFullLink;
			_tapCommand = new RelayCommand(() =>
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

		public ICommand TapCommand
		{
			get
			{
				return _tapCommand;
			}
		}
	}
}
