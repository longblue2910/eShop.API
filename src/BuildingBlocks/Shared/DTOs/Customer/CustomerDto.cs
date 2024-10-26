using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Customer
{
    public class CustomerDto
    {
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
