using Repository.Models.DataTransferObjects;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IIncidentService
    {
        Task CreateIncident(IncidentDTO incident);
    }
}