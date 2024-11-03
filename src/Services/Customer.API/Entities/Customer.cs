using Contracts.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.API.Entities
{
    public class Customer: EntityBase<int>
    {
        [Required]
        [MaxLength(100)]
        public string? UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string? FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string? EmailAddress { get; init; }
    }
}