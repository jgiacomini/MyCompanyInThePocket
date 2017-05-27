using System;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.Services;
using UIKit;

namespace MyCompanyInThePocket.iOS.Services
{
    public class iOSAlertService : IAlertService
    {
        public async Task<bool> ShowInformationAsync(string title, string message, string okMessage)
        {
            bool result = false;

            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create(okMessage, UIAlertActionStyle.Default, (obj) => result = true));
            await CurrentViewController.Current.PresentViewControllerAsync(alertController, true);

            return result;
        }


        public async Task ShowExceptionMessageAsync(Exception ex, string message)
        {

#if DEBUB
            message += "  " + ex.Message;
#endif

            var alertController = UIAlertController.Create("Une erreur est survenue", message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			await CurrentViewController.Current.PresentViewControllerAsync(alertController, true);

		}


        public async Task<bool> ShowAlertAsync(string title, string message, string okMessage, string cancelMessage)
        {
            bool result = false;

            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create(okMessage, UIAlertActionStyle.Default, (obj) => result = true));
            alertController.AddAction(UIAlertAction.Create(cancelMessage, UIAlertActionStyle.Cancel, (obj) => result = false));
            await CurrentViewController.Current.PresentViewControllerAsync(alertController, true);

            return result;
        }

        public async Task<bool> ShowDestructiveAlertAsync(string title, string message, string destructiveMessage, string cancelMessage)
        {
			bool result = false;

            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.ActionSheet);
            alertController.AddAction(UIAlertAction.Create(destructiveMessage, UIAlertActionStyle.Destructive, (obj) => result = true));
			alertController.AddAction(UIAlertAction.Create(cancelMessage, UIAlertActionStyle.Cancel, (obj) => result = false));
			await CurrentViewController.Current.PresentViewControllerAsync(alertController, true);

			return result;
        }
    }
}
