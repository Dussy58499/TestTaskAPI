using Repository.Data;
using Repository.Interfaces;
using Repository.Models.Domain;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly AppDbContext _context;

        public IncidentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddIncidentAsync(Incident incident)
        {
            await _context.Incidents.AddAsync(incident);
            await _context.SaveChangesAsync();
        }
    }
}
