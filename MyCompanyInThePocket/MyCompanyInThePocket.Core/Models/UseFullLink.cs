using SQLite.Net.Attributes;

namespace MyCompanyInThePocket.Core.Models
{
	public class UseFullLink
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Link { get; set; }

		public string Name { get; set; }

		public byte[] Icon { get; set; }
	}
}
