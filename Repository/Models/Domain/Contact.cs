using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models.Domain
{
    public class Contact
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; } //unique identifier

        [StringLength(50, ErrorMessage = "FirstName cannot be longer than 500 characters.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "LastName cannot be longer than 500 characters.")]
        public string LastName { get; set; }

        [ForeignKey("AccountName")]
        public string AccountName { get; set; }
        public Account Account { get; set; }
    }
}
