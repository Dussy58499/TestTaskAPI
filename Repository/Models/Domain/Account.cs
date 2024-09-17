using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models.Domain
{
    public class Account
    {
        [Key]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }  //unique string field
        public ICollection<Contact> Contacts { get; set; }

        [ForeignKey("IncidentName")]
        public string IncidentName { get; set; }
        public Incident Incident { get; set; }
    }
}