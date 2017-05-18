using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.ViewModels
{
    public abstract class BaseViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Fields
        private bool _isBusy;
        #endregion

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                Set(ref _isBusy, value);
            }
        }

        public void ShowViewModel<T>() where T : BaseViewModel
        {
            App.Instance.NavigationService.ShowViewMode<T>();
        }

    }
}
