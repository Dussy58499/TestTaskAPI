using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models.DataTransferObjects;
using Repository.Models.Domain;
using Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task CreateContact(ContactDTO contactDto)
        {
            try
            {
                var contact = new Contact
                {
                    Email = contactDto.Email,
                    FirstName = contactDto.FirstName,
                    LastName = contactDto.LastName
                };

                await _contactRepository.AddContactAsync(contact);
            }

            catch (ArgumentException ex)
            {
                throw new ApplicationException("Creating Contact error");
            }
        }
    }
}
