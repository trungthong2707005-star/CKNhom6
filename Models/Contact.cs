using System.ComponentModel.DataAnnotations;

namespace ContactApp.API.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Phone] // Validate định dạng số điện thoại
        public string Phone { get; set; }
        [Required]
        [EmailAddress] // Validate định dạng Email
        public string Email { get; set; }
        public string Note { get; set; }
    }
}