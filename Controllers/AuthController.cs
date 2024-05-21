using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Validation;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace study4_be.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserRepository _userRepository = new UserRepository();

        private STUDY4Context _context = new STUDY4Context();
        private UserRegistrationValidator _userRegistrationValidator = new UserRegistrationValidator();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                var user = JsonSerializer.Deserialize<User>(requestBody);

                if (user != null)
                {
                    string errorMessage;
                    if (_userRegistrationValidator.Validate(user, out errorMessage))
                    {
                        _userRepository.AddUser(user);
                        return Json(new { status = 200, message = "User registered successfully", userData = user });
                    }
                    else
                    {
                        return BadRequest(errorMessage);
                    }
                }
                else
                {
                    return BadRequest("Invalid user data");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                var loginData = JsonSerializer.Deserialize<User>(requestBody);

                if (loginData != null && !string.IsNullOrEmpty(loginData.UsersEmail) && !string.IsNullOrEmpty(loginData.UsersPassword))
                {
                    var user = _userRepository.GetUserByUserEmail(loginData.UsersEmail);

                    if (user != null && _userRepository.VerifyPassword(loginData.UsersPassword, user.UsersPassword))
                    {
                        return Json(new { status = 200, message = "Login successful", userData = user });
                    }
                    else
                    {
                        return Unauthorized("Invalid username or password");
                    }
                }
                else
                {
                    return BadRequest("Invalid login data");
                }
            }
        }
    }
}
