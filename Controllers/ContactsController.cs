using ContactApp.API.DTOs;
using ContactApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        // Giả lập Database
        private static List<Contact> _contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "Nguyen Van A", Phone = "0901234567", Email = "a@test.com", Note = "Khach vip" },
            new Contact { Id = 2, Name = "Tran Thi B", Phone = "0909998887", Email = "b@test.com", Note = "Moi quen" }
        };
        private const string API_KEY = "MySecretKey123"; // Key xác thực đơn giản

        // Helper check Auth (cho các method POST, PUT, DELETE)
        private bool IsAuthenticated()
        {
            // Lấy header x-api-key
            if (!Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
            {
                return false;
            }
            return API_KEY.Equals(extractedApiKey);
        }

        // 1. GET: Lấy danh sách (Search + Pagination) - Public
        [HttpGet]
        public IActionResult GetContacts([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            //throw new Exception("Demo lỗi 500 cho thầy xem");
            var query = _contacts.AsQueryable();

            // Search logic
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(search) || c.Phone.Contains(search));
            }

            // Pagination logic
            int totalItems = query.Count();
            var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Data = data
            });
        }

        // 2. POST: Thêm mới - Cần Auth
        [HttpPost]
        public IActionResult CreateContact([FromBody] ContactDto model)
        {
            if (!IsAuthenticated()) return Unauthorized("Thiếu hoặc sai API Key");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newContact = new Contact
            {
                Id = _contacts.Count > 0 ? _contacts.Max(c => c.Id) + 1 : 1,
                Name = model.Name,
                Phone = model.Phone,
                Email = model.Email,
                Note = model.Note
            };

            _contacts.Add(newContact);
            return CreatedAtAction(nameof(GetContacts), new { id = newContact.Id }, newContact);
        }

        // 3. PUT: Cập nhật - Cần Auth
        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] ContactDto model)
        {
            if (!IsAuthenticated()) return Unauthorized("Thiếu hoặc sai API Key");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound($"Không tìm thấy ID {id}");

            contact.Name = model.Name;
            contact.Phone = model.Phone;
            contact.Email = model.Email;
            contact.Note = model.Note;

            return Ok(contact);
        }

        // 4. DELETE: Xóa - Cần Auth
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            if (!IsAuthenticated()) return Unauthorized("Thiếu hoặc sai API Key");

            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound();

            _contacts.Remove(contact);
            return Ok(new { Message = "Đã xóa thành công" });
        }
    }
}