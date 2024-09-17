using Repository.Models.DataTransferObjects;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IContactService
    {
        Task CreateContact(ContactDTO contact);
    }
}
