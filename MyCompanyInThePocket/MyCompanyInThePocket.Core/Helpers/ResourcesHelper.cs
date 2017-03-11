using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MyCompanyInThePocket.Core
{
	public class ResourcesHelper
	{
		public static Stream GetEmbeddedResourceStream(Assembly assembly, string resourceFileName)
		{
			var resourceNames = assembly.GetManifestResourceNames();

			var resourcePath = resourceNames.Single(x => x.Equals(resourceFileName));

			return assembly.GetManifestResourceStream(resourcePath);
		}

		public static byte[] GetEmbeddedResourceBytes(Assembly assembly, string resourceFileName)
		{
			var stream = GetEmbeddedResourceStream(assembly, resourceFileName);

			using (var memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}
}
