using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core
{
    public class UseFullLinksViewModel : BaseViewModel
	{
		private readonly IUseFullLinkService _useFullLinkService;

		public UseFullLinksViewModel()
		{
            _useFullLinkService = App.Instance.GetInstance<IUseFullLinkService>();
            UseFullLinks = new SuspendableObservableCollection<UseFullLinkViewModel>();

        }

		public SuspendableObservableCollection<UseFullLinkViewModel> UseFullLinks
		{
			get;
			private set;
		}

		public async Task InitializeAsync()
		{
			IsBusy = true;
			try
			{
                UseFullLinks.PauseNotifications();
                UseFullLinks.Clear();
                var useFullLinks = await _useFullLinkService.GetUseFullLinksAsync();
                UseFullLinks.AddRange(useFullLinks.Select(l => new UseFullLinkViewModel(l)));
			}
			catch (Exception ex)
			{
                App.Instance.MessageService.ShowErrorToastAsync(ex, "Erreur lors de la récupération des liens utiles.");
				// TODO : log something
			}
			finally
			{
				IsBusy = false;
                UseFullLinks.ResumeNotifications();
			}
		}
	}
}
