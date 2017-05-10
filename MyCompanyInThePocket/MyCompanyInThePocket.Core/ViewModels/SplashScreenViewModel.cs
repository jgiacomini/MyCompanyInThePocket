using MyCompanyInThePocket.Core.Services;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.ViewModels
{
    /// <summary>
    /// Splash screen view model.
    /// </summary>
    public class SplashScreenViewModel : BaseViewModel
    {
        #region Fields
        private string _currentState;
        private string _errorMessage;
		private bool _hasError;
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

                if (ApplicationSettings.LaunchStartupScreen)
				{
					ShowViewModel<StartupViewModel>();
				}
				else
				{
					ShowViewModel<MainViewModel>();
				}
            }
            catch (System.Exception ex)
            {
				HasError = true;
                ErrorMessage = "Erreur durant l'initialisation de l'application";
                App.Instance.MessageService.ShowErrorToastAsync(ex, "Erreur durant l'initialisation de l'application");
                // TODO : log something
            }
            finally
            {
                IsBusy = false;
            }

        }

		public bool HasError
		{
			get { return _hasError; }
			set { Set(ref _hasError, value); }
		}

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { Set(ref _errorMessage, value); }
        }

        public string CurrentState
        {
            get { return _currentState; }
            set { Set(ref _currentState, value); }
        }

    }
}
