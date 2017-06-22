using System;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
	public interface IAuthentificationService
	{
		Task AuthenticateAsync(bool silentForced = false);
		void Disconnect();
		Task<T> GetAsync<T>(string route)
			where T : class;

        Task<byte[]> GetBytesAsync(string route);

	}
}