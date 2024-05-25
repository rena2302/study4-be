using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Validation;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace study4_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserRepository _userRepository = new UserRepository();

        private STUDY4Context _context = new STUDY4Context();
        private UserRegistrationValidator _userRegistrationValidator = new UserRegistrationValidator();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Register")]
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
        [HttpPost("Login")]
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
                        return Json(new { status = 200, message = "Login successful", user });
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
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Json(new { status = 200, message = "Get Users Successful", users });

        }
        //development enviroment
        [HttpDelete("DeleteAllUsers")]
        public async Task<IActionResult> DeleteAllUsers()
        {
            await _userRepository.DeleteAllUsersAsync();
            return Json(new { status = 200, message = "Delete Users Successful" });
        }
    }
}
