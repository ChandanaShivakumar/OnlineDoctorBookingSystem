using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class Registration
    {
        [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
