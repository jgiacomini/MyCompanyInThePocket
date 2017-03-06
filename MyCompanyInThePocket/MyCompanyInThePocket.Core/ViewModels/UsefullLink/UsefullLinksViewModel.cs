using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MyCompanyInThePocket.Core.Services.Interface;
using MyCompanyInThePocket.Core.ViewModels;

namespace MyCompanyInThePocket.Core
{
	public class UseFullLinksViewModel : BaseViewModel
	{
		private readonly IUseFullLinkService _useFullLinkService;

		public UseFullLinksViewModel(IUseFullLinkService useFullLinkService)
		{
			_useFullLinkService = useFullLinkService;
		}

		public ObservableCollection<UseFullLinkViewModel> UseFullLinks
		{
			get;
			private set;
		}

		public async Task InitializeAsync()
		{
			IsBusy = true;
			try
			{
				var useFullLinks = await _useFullLinkService.GetUseFullLinksAsync();
				UseFullLinks = new ObservableCollection<UseFullLinkViewModel>(useFullLinks.Select(l => new UseFullLinkViewModel(l)));
				RaisePropertyChanged(nameof(UseFullLinks));
			}
			catch (Exception ex)
			{
				await Mvx.Resolve<IMessageService>()
					 .ShowErrorToastAsync(ex, "Erreur lors de la récupération des liens utiles.");
				// TODO : log something
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
