using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Models
{
    public class Meeting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public MeetingType Type { get; set; }

        public bool AllDayEvent { get; set; }
        public bool IsRecurrent { get; internal set; }

        [Ignore]
        public TimeSpan Duration => EndDate - StartDate;
    }


    public enum MeetingType
    {
        Mission,
        AvantVente,
        CP_RTT,
        Conference,
        NonFacture,
        Unknown
    }
}
