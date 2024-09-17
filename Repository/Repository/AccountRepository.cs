using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Repository.Data;
using Repository.Interfaces;
using Repository.Models.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetAccountByNameAsync(string name)
        {
            return await _context.Accounts
                .Include(a => a.Contacts)
                .FirstOrDefaultAsync(a => a.Name == name);
        }

        public bool AccountExists(string name)
        {
            return _context.Accounts.Any(a => a.Name == name);
        }
    }
}
