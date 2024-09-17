using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models.Domain
{
    public class Incident
    {
        [Key]
        public string Name { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
