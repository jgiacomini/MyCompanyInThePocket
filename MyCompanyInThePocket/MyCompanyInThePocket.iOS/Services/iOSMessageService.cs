using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MyCompanyInThePocket.Core.Services.Interface;

namespace MyCompanyInThePocket.iOS
{
	public class iOSMessageService : IMessageService
	{
		public Task ShowErrorToastAsync(Exception exception, string message)
		{
			// TODO : display a popup here...

			return Task.FromResult(true);
		}
	}
}
