using System;
using System.Threading.Tasks;
using UIKit;
using MyCompanyInThePocket.Core.Services;

namespace MyCompanyInThePocket.iOS
{
	public class iOSMessageService : IMessageService
	{
		public Task ShowErrorToastAsync(Exception exception, string message)
		{
			var alertView = new UIAlertView("", message
			                                     #if DEBUG
                    								+ ": " + exception.Message
												 #endif
			                                     , null, "Ok", null);
			alertView.Show();
			return Task.FromResult(true);
		}
	}
}
