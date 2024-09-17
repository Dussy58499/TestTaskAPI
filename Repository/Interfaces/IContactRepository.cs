using Repository.Models.Domain;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IContactRepository
    {
        Task AddContactAsync(Contact contact);
        Task<Contact> GetContactByEmailAsync(string email);
    }
}