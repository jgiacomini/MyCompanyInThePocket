using System;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.Services;
using UIKit;

namespace MyCompanyInThePocket.iOS.Services
{
    public class iOSAlertService : IAlertService
    {
        public async Task ShowInformationAsync(string title, string message, string okMessage)
        {
            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create(okMessage, UIAlertActionStyle.Default, null));
            await CurrentViewController.Current.PresentViewControllerAsync(alertController, true);
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


        public Task<bool> ShowAlertAsync(string title, string message, string okMessage, string cancelMessage)
        {
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

			var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create(okMessage, UIAlertActionStyle.Default, (obj) => tcs.SetResult(true)));
            alertController.AddAction(UIAlertAction.Create(cancelMessage, UIAlertActionStyle.Cancel, (obj) => tcs.SetResult(false)));
            CurrentViewController.Current.PresentViewController(alertController, true, null);

			return tcs.Task;
        }

        public Task<bool> ShowDestructiveAlertAsync(string title, string message, string destructiveMessage, string cancelMessage)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			
            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.ActionSheet);
            alertController.AddAction(UIAlertAction.Create(destructiveMessage, UIAlertActionStyle.Destructive, (obj) => tcs.SetResult(true)));
            alertController.AddAction(UIAlertAction.Create(cancelMessage, UIAlertActionStyle.Cancel, (obj) => tcs.SetResult(false)));
            CurrentViewController.Current.PresentViewController(alertController, true, null);

            return tcs.Task;
        }
    }
}
