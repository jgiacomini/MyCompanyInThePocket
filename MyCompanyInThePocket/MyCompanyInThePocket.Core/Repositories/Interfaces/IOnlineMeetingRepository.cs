using MyCompanyInThePocket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Repositories.Interfaces
{
    public interface IOnlineMeetingRepository
    {
        Task<List<Meeting>> GetMeetingAsync();
    }
}
