using System.ComponentModel.DataAnnotations;

namespace Repository.Models.DataTransferObjects
{
    public class AccountDTO
    {
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }
    }
}
