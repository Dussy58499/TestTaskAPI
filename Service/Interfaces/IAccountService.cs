using Repository.Models.DataTransferObjects;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAccountService
    {
        Task CreateAccount(AccountDTO account);
        bool HasAccount(string name);
    }
}