using Repository.Interfaces;
using Repository.Models.DataTransferObjects;
using Repository.Models.Domain;
using Service.Interfaces;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace Service.Services
{

    public class IncidentService : IIncidentService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService(IAccountRepository accountRepository, IContactRepository contactRepository, IIncidentRepository incidentRepository)
        {
            _accountRepository = accountRepository;
            _contactRepository = contactRepository;
            _incidentRepository = incidentRepository;
        }

        public async Task CreateIncident(IncidentDTO incidentDto)
        {
            try
            {

                var account = await _accountRepository.GetAccountByNameAsync(incidentDto.AccountName);
                if (account == null)
                    throw new Exception("Account not found");

                var contact = await _contactRepository.GetContactByEmailAsync(incidentDto.ContactEmail);
                if (contact == null)
                {
                    contact = new Contact
                    {
                        Email = incidentDto.ContactEmail,
                        FirstName = incidentDto.ContactFirstName,
                        LastName = incidentDto.ContactLastName,
                        AccountName = account.Name
                    };

                    await _contactRepository.AddContactAsync(contact);
                }

                var incident = new Incident
                {
                    Description = incidentDto.Description,
                    Accounts = new List<Account> { account }
                };

                account.IncidentName = incident.Name;
                await _incidentRepository.AddIncidentAsync(incident);
            }

            catch (ArgumentException ex)
            {
                throw new ApplicationException("Creating Incident error" + ex.Message);
            }

            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error" + ex.Message);
            }
        }
    }
}
