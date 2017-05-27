using System;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
    public interface IAlertService
    {
        Task<bool> ShowInformationAsync(string title, string message, string okMessage);
        Task<bool> ShowAlertAsync(string title, string message, string okMessage, string cancelMessage);
        Task ShowExceptionMessageAsync(Exception ex, string message);
		Task<bool> ShowDestructiveAlertAsync(string title, string message, string destructiveMessage, string cancelMessage);
    }
}
