using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace study4_be.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserRepository _userRepository = new UserRepository();

        private  STUDY4Context _context = new STUDY4Context();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            // Lấy dữ liệu từ request body
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();

                // Chuyển đổi JSON thành đối tượng User
                var user = JsonSerializer.Deserialize<User>(requestBody);

                // Kiểm tra dữ liệu và thực hiện các thao tác cần thiết
                if (user != null)
                {
                    // Thêm người dùng vào cơ sở dữ liệu
                    _userRepository.AddUser(user);
                    // Trả về kết quả thành công
                    return Json(new { status = "OK", message = "User registered successfully", userData = user });
                }
                else
                {
                    // Trả về lỗi nếu dữ liệu không hợp lệ
                    return BadRequest("Invalid user data");
                }
            }

        }
    }
}
