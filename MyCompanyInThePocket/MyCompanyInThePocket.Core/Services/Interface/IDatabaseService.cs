using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
    public interface IDatabaseService
    {
        Task InitializeDbAsync();
    }
}