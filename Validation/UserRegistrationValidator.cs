using Microsoft.EntityFrameworkCore;
using study4_be.Models;
using study4_be.Repositories;

namespace study4_be.Validation
{
    public class UserRegistrationValidator
    {
        private readonly UserRepository _userRepository = new UserRepository();
        public bool Validate(User user, out string errorMessage)
        {
            if (string.IsNullOrEmpty(user.Email) || !IsValidEmail(user.Email))
            {
                errorMessage = "A valid email is required.";
                return false;
            }
            if (_userRepository.CheckEmailExists(user.Email))
            {
                errorMessage = "Email already exists.";
                return false;
            }
            if (string.IsNullOrEmpty(user.UsersPassword))
            {
                errorMessage = "Password is required.";
                return false;
            }
            errorMessage = null;
            return true;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
