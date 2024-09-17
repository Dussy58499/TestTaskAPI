using Repository.Models.Domain;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IIncidentRepository
    {
        Task AddIncidentAsync(Incident incident);
    }
}