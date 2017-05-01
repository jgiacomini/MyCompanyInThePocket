using System;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
	public interface IAuthentificationService
	{
		Task AuthenticateAsync();
		void Disconnect();
		Task<T> GetAsync<T>(string route)
			where T : class;
	}
}
