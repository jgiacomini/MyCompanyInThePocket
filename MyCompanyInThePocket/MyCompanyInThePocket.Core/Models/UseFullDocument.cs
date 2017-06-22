using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Models
{
    public class UseFullDocument : SimpleUseFullDocument
    {
        public byte[] Data { get; set; }
    }

    public class SimpleUseFullDocument
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
		public string Created { get; set; }
        public string Modified { get; set; }
		public string Url { get; set; }
        public string Extension { get; set; }
    }
}
