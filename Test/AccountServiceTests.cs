using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Repository.Interfaces;
using Service.Services;
using Repository.Models.DataTransferObjects;
using Repository.Models.Domain;

namespace Test
{
    [TestClass]
    public class AccountServiceTests
    {
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<IContactRepository> _contactRepositoryMock;
        private AccountService _accountService;

        [TestInitialize]
        public void Setup()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _contactRepositoryMock = new Mock<IContactRepository>();
            _accountService = new AccountService(_accountRepositoryMock.Object, _contactRepositoryMock.Object);
        }

        [TestMethod]
        public async Task CreateAccount_OK()
        {
            // Arrange
            var accountDto = new AccountDTO
            {
                Name = "TestAccount",
                ContactFirstName = "John",
                ContactLastName = "Doe",
                ContactEmail = "john.doe@example.com"
            };

            _contactRepositoryMock.Setup(x => x.GetContactByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Contact)null);

            _accountRepositoryMock.Setup(x => x.AddAccountAsync(It.IsAny<Account>()))
                .Returns(Task.CompletedTask);

            // Act
            await _accountService.CreateAccount(accountDto);

            // Assert
            _contactRepositoryMock.Verify(x => x.AddContactAsync(It.IsAny<Contact>()), Times.Once);
            _accountRepositoryMock.Verify(x => x.AddAccountAsync(It.IsAny<Account>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateAccount_BAD()
        {
            // Arrange
            var accountDto = new AccountDTO
            {
                Name = "TestAccount",
                ContactFirstName = "John",
                ContactLastName = "Doe",
                ContactEmail = "john.doe@example.com"
            };

            _contactRepositoryMock.Setup(x => x.GetContactByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new Contact()); // Contact exists

            _accountRepositoryMock.Setup(x => x.AddAccountAsync(It.IsAny<Account>()))
                .Throws(new ApplicationException("Database error"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() => _accountService.CreateAccount(accountDto));
        }

        [TestMethod]
        public void HasAccount_OK()
        {
            // Arrange
            var accountName = "TestAccount";
            _accountRepositoryMock.Setup(x => x.AccountExists(accountName)).Returns(true);

            // Act
            var result = _accountService.HasAccount(accountName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasAccount_BAD()
        {
            // Arrange
            var accountName = "TestAccount";
            _accountRepositoryMock.Setup(x => x.AccountExists(accountName)).Returns(false);

            // Act
            var result = _accountService.HasAccount(accountName);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
