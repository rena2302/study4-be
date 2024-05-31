using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Validation;
using NuGet.Protocol.Core.Types;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth_APIController : Controller
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

                if (loginData != null && !string.IsNullOrEmpty(loginData.UserEmail) && !string.IsNullOrEmpty(loginData.UserPassword))
                {
                    var user = _userRepository.GetUserByUserEmail(loginData.UserEmail);

                    if (user != null && _userRepository.VerifyPassword(loginData.UserPassword, user.UserPassword))
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
        [HttpGet("Get_AllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> Get_AllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Json(new { status = 200, message = "Get Users Successful", users });

        }
        //development enviroment
        [HttpDelete("Delete_AllUsers")]
        public async Task<IActionResult> Delete_AllUsers()
        {
            await _userRepository.DeleteAllUsersAsync();
            return Json(new { status = 200, message = "Delete Users Successful" });
        }
    }
}
