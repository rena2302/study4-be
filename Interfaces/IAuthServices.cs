using Microsoft.AspNetCore.Identity;
using study4_be.Models.DTO;

namespace study4_be.Interfaces
{
    public interface IAuthServices
    {
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto registerUserDto);
    }
}
