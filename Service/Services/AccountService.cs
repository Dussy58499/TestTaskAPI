using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models.DataTransferObjects;
using Repository.Models.Domain;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IContactRepository _contactRepository;

        public AccountService(IAccountRepository accountRepository, IContactRepository contactRepository)
        {
            _accountRepository = accountRepository;
            _contactRepository = contactRepository;
        }

        public async Task CreateAccount(AccountDTO accountDto)
        {
            try
            {
                var account = new Account { Name = accountDto.Name };
                var contact = await _contactRepository.GetContactByEmailAsync(accountDto.ContactEmail);

                if (contact == null)
                {
                    contact = new Contact
                    {
                        FirstName = accountDto.ContactFirstName,
                        LastName = accountDto.ContactLastName,
                        Email = accountDto.ContactEmail
                    };
                    await _contactRepository.AddContactAsync(contact);
                }

                account.Contacts = new List<Contact> { contact };
                await _accountRepository.AddAccountAsync(account);
            }

            catch (ArgumentException ex)
            {
                throw new ApplicationException("Creating account error" + ex.Message);
            }
        }

        public bool HasAccount(string name)
        {
            return _accountRepository.AccountExists(name);
        }
    }
}
