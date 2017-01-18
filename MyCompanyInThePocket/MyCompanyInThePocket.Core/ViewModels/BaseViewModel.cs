using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        #region Fields
        private bool _isBusy;
        #endregion

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }

        }
    }
}
