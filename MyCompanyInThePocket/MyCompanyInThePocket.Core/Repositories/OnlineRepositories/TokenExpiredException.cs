using System;
namespace MyCompanyInThePocket.Core
{
	public class TokenExpiredException : Exception
	{
		public TokenExpiredException() :
			base("Le token utilisateur est expiré")
		{
		}
	}
}
