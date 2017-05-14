using MyCompanyInThePocket.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
    public interface INavigationService
    {
        void ShowViewMode<T>() where T : BaseViewModel;
    }
}
