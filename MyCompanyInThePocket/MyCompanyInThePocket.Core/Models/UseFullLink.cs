using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Models
{
    public class UseFullLink
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Link { get; set; }

        public string Name { get; set; }

        public byte[] Icon { get; set; }
    }
}
