using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Models.Entities
{
    public class Customer
    {

        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }
    }
}
