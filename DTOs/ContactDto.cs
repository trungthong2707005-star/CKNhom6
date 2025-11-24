// DTOs/ContactDto.cs
using System.ComponentModel.DataAnnotations;

namespace ContactApp.API.DTOs
{
    public class ContactDto
    {
        [Required(ErrorMessage = "Tên là bắt buộc")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SĐT là bắt buộc")]
        [Phone(ErrorMessage = "SĐT không hợp lệ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        public string Note { get; set; }
    }
}