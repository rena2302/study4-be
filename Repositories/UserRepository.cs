using study4_be.Models;
using System.Linq;

namespace study4_be.Repositories
{
    public class UserRepository
    {
        private readonly STUDY4Context _context = new STUDY4Context();

        public User GetUserByUsername(string username)
        {
            // Truy vấn người dùng từ cơ sở dữ liệu sử dụng tên người dùng
            return _context.Users.FirstOrDefault(u => u.UsersName == username);
        }

        public void AddUser(User user)
        {
            // Thêm người dùng mới vào cơ sở dữ liệu và lưu thay đổi
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Bạn có thể triển khai các phương thức khác tương tự ở đây nếu cần

        // Ví dụ:
        // public IEnumerable<User> GetAllUsers()
        // {
        //     return _context.Users.ToList();
        // }
    }
}
