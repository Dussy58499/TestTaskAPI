using Repository.Models.Domain;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task AddAccountAsync(Account account);
        Task<Account> GetAccountByNameAsync(string name);
        bool AccountExists(string name);
    }
}