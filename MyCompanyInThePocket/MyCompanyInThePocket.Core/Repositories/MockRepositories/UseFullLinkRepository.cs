using MyCompanyInThePocket.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.Models;
using System.Reflection;

namespace MyCompanyInThePocket.Core.Repositories.MockRepositories
{
	internal class UseFullLinkRepository : IOnlineUseFullLinkRepository
	{
		public async Task<List<UseFullLink>> GetUseFullLinksAsync()
		{
			var assembly = typeof(UseFullLinkRepository).GetTypeInfo().Assembly;
			List<UseFullLink> useFullLinks = new List<UseFullLink>();

			await Task.Run(() =>
			{
				useFullLinks.Add(new UseFullLink { Link = "http://mycompany.sharepoint.com", Name = "Espace Projets", Icon = ResourcesHelper.GetEmbeddedResourceBytes(assembly, "MyCompanyInThePocket.Core.Resources.Images.projets.png") });
				useFullLinks.Add(new UseFullLink { Link = "http://outlook.office365.com/owa", Name = "Outlook (Web)", Icon = ResourcesHelper.GetEmbeddedResourceBytes(assembly, "MyCompanyInThePocket.Core.Resources.Images.outlook.png") });
				useFullLinks.Add(new UseFullLink { Link = "http://portal.office.com", Name = "Portail Office 365", Icon = ResourcesHelper.GetEmbeddedResourceBytes(assembly, "MyCompanyInThePocket.Core.Resources.Images.office365.png") });
				useFullLinks.Add(new UseFullLink { Link = "http://mycompany.visualstudio.com", Name = "VSTS", Icon = ResourcesHelper.GetEmbeddedResourceBytes(assembly, "MyCompanyInThePocket.Core.Resources.Images.vsts.png") });
				useFullLinks.Add(new UseFullLink { Link = "http://portal.azure.com", Name = "Azure", Icon = ResourcesHelper.GetEmbeddedResourceBytes(assembly, "MyCompanyInThePocket.Core.Resources.Images.azure.png") });
				useFullLinks.Add(new UseFullLink { Link = "http://mycompany.delve.com", Name = "Delve", Icon = ResourcesHelper.GetEmbeddedResourceBytes(assembly, "MyCompanyInThePocket.Core.Resources.Images.delve.png") });
			});

			return useFullLinks;
		}
	}
}
