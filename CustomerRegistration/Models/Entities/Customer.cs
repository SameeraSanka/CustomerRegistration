using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRegistration.Models.Entities;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public int PostalCode { get; set; }

    public bool IsDeleted { get; set; } = false;
}
