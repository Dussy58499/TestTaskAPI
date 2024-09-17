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
    public class ContactServiceTests
    {
        private Mock<IContactRepository> _contactRepositoryMock;
        private ContactService _contactService;

        [TestInitialize]
        public void Setup()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _contactService = new ContactService(_contactRepositoryMock.Object);
        }

        [TestMethod]
        public async Task CreateContact_OK()
        {
            // Arrange
            var contactDto = new ContactDTO
            {
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe"
            };

            _contactRepositoryMock.Setup(x => x.AddContactAsync(It.IsAny<Contact>()))
                .Returns(Task.CompletedTask);

            // Act
            await _contactService.CreateContact(contactDto);

            // Assert
            _contactRepositoryMock.Verify(x => x.AddContactAsync(It.IsAny<Contact>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateContact_BAD()
        {
            // Arrange
            var contactDto = new ContactDTO
            {
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe"
            };

            _contactRepositoryMock.Setup(x => x.AddContactAsync(It.IsAny<Contact>()))
                .Throws(new ApplicationException("Database error"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationException>(() => _contactService.CreateContact(contactDto));
        }
    }
}
