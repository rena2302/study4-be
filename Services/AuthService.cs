using Microsoft.AspNetCore.Identity;
using study4_be.Interfaces;
using study4_be.Models.DTO;

namespace study4_be.Services
{
    public class AuthService /*: IAuthService*/
    {
        //private readonly UserManager<Models.User> _userManager;

        //public AuthService(UserManager<Models.User> userManager)
        //{
        //    _userManager = userManager;
        //}

        //public async Task<IdentityResult> RegisterUserAsync(RegisterUserDto registerUserDto)
        //{
        //    var user = new Models.User
        //    {
        //        Name = registerUserDto.UserName,
        //        Email = registerUserDto.Email
        //    };

        //    var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        //    return result;
        //}
    }
}
