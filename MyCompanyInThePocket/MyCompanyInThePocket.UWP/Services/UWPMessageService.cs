using System;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.Services;

namespace MyCompanyInThePocket.UWP.Services
{
	public class UWpMessageService : IMessageService
	{
		public Task ShowErrorToastAsync(Exception exception, string message)
		{
            //TODO:
			return Task.FromResult(true);
		}
	}
}
