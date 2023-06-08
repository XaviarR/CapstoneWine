using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CapstoneWine.Models
{
    public class CustomerModel
    {
        [Key]
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetAddress { get; set; }

        public string? Suburb { get; set; }
        public string? City { get; set; }
        public string? PostCode { get; set; }

        [ForeignKey("ID")]
        public string IdentityKey { get; set; } //email

        //public IdentityUser? IdentityUser { get; set; }

       // public IdentityUser? IdentityUser { get; set; }

    }
}
