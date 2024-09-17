using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Repository.Interfaces;
using Repository.Models.DataTransferObjects;
using Repository.Models.Domain;
using Service.Services;

namespace Test
{
    [TestClass]
    public class IncidentServiceTests
    {
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<IContactRepository> _contactRepositoryMock;
        private Mock<IIncidentRepository> _incidentRepositoryMock;
        private IncidentService _incidentService;

        [TestInitialize]
        public void Setup()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _contactRepositoryMock = new Mock<IContactRepository>();
            _incidentRepositoryMock = new Mock<IIncidentRepository>();
            _incidentService = new IncidentService(
                _accountRepositoryMock.Object,
                _contactRepositoryMock.Object,
                _incidentRepositoryMock.Object
            );
        }

        [TestMethod]
        public async Task CreateIncident_AccountNotFound_OK()
        {
            // Arrange
            var incidentDto = new IncidentDTO { AccountName = "NonExistentAccount" };
            _accountRepositoryMock.Setup(x => x.GetAccountByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((Account)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() => _incidentService.CreateIncident(incidentDto));
        }

        [TestMethod]
        public async Task CreateIncident_CreateIncident_OK()
        {
            // Arrange
            var account = new Account { Name = "TestAccount" };
            var contact = new Contact { Email = "test@example.com" };
            var incidentDto = new IncidentDTO
            {
                AccountName = account.Name,
                ContactEmail = contact.Email,
                Description = "Incident description"
            };

            _accountRepositoryMock.Setup(x => x.GetAccountByNameAsync(account.Name))
                .ReturnsAsync(account);
            _contactRepositoryMock.Setup(x => x.GetContactByEmailAsync(contact.Email))
                .ReturnsAsync(contact);
            _incidentRepositoryMock.Setup(x => x.AddIncidentAsync(It.IsAny<Incident>()))
                .Returns(Task.CompletedTask);

            // Act
            await _incidentService.CreateIncident(incidentDto);

            // Assert
            _incidentRepositoryMock.Verify(x => x.AddIncidentAsync(It.IsAny<Incident>()), Times.Once);
        }
    }
}
