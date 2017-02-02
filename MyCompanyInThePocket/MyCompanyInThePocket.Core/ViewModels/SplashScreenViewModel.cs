using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.Services.Interface;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.ViewModels
{
    public class SplashScreenViewModel : BaseViewModel
    {
        #region Fields
        private string _currentState;
        private string _errorMessage;
        private readonly IDatabaseService _databaseService;
        #endregion

        public SplashScreenViewModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;
            try
            {
                CurrentState = "Initialisation";
                await _databaseService.InitializeDbAsync();
                var meetingService = Mvx.Resolve<IMeetingService>();
                var meetings = await meetingService.GetMeetingsAsync();

                // l'application est initialisé on ouvre la page principale.
                ShowViewModel<MainScreenViewModel>();
            }
            catch (System.Exception ex)
            {
                ErrorMessage = "Erreur durant l'initialisation de l'application";
                // TODO : log something
            }
            finally
            {
                IsBusy = false;
            }

        }


        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public string CurrentState
        {
            get { return _currentState; }
            set { SetProperty(ref _currentState, value); }
        }

    }
}
