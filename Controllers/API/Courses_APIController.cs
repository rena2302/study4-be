using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Validation;
using NuGet.Protocol.Core.Types;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using study4_be.Services;
using Microsoft.EntityFrameworkCore;
using study4_be.Services.Request;
using System.Linq;
namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Courses_APIController : Controller
    {
        private readonly ILogger<Courses_APIController> _logger;
        public Courses_APIController(ILogger<Courses_APIController> logger)
        {
            _logger = logger;
        }
        private readonly CourseRepository _coursesRepository = new CourseRepository();
        public STUDY4Context _context = new STUDY4Context();

        [HttpGet("Get_AllCourses")] 
        public async Task<ActionResult<IEnumerable<Course>>> Get_AllCourses()
        {
            var courses = await _coursesRepository.GetAllCoursesAsync();
            return Json(new { status = 200, message = "Get Courses Successful", courses });
        }

        [HttpPost("Get_UnregisteredCourses")] // courses the user hasn't registered for
        public async Task<ActionResult> Get_UnregisteredCourses(GetUserCoursesRequest request)
        {
            List<int> registeredCourseIds = await GetCoursesUserHasPurchasedAsync(request.userId);

            var unregisteredCourses = await _context.Courses
                .Where(c => !registeredCourseIds.Contains(c.CourseId)) // Sử dụng CourseId từ bảng Courses
                .ToListAsync();

            if (unregisteredCourses == null || !unregisteredCourses.Any())
            {
                return NotFound(new { status = 404, message = "No unregistered courses found or User Register All Before" });
            }

            return Ok(new { status = 200, message = "Get Unregistered Courses Successful", unregisteredCourses });
        }
        [HttpPost("Get_OutstandingCoursesUserNotBought")]
        public async Task<ActionResult> Get_OutstandingCoursesUserNotBought(GetUserCoursesRequest req)
        {
            List<int>  userPurchasedCourses = await GetCoursesUserHasPurchasedAsync(req.userId);
            var outstandingCourses = await _context.UserCourses
                .Where(uc => !userPurchasedCourses.Contains(uc.CourseId)) // Lọc các khóa học chưa mua
                .GroupBy(uc => uc.CourseId) // Nhóm theo CourseId
                .OrderByDescending(g => g.Count()) // Sắp xếp giảm dần theo số lần xuất hiện
                .Select(g => g.Key)// Chọn ra CourseId
                .Take(req.amountOutstanding)
                .ToListAsync();

            // Lấy thông tin chi tiết của các khóa học nổi bật
            var detailedOutstandingCourses = await _context.Courses
                .Where(c => outstandingCourses.Contains(c.CourseId)) // Lọc các khóa học theo danh sách các CourseId nổi bật
                .ToListAsync();
                
            if (detailedOutstandingCourses == null || !detailedOutstandingCourses.Any())
                {
                    return NotFound(new { status = 404, message = "No outstanding courses found or not have any outstaind course" });
                }
            return Ok(new { status = 200, message = "Get Outstanding Courses User Hadn't Bought Successful", outstandingCourses = detailedOutstandingCourses });
        }
        public async Task<List<int>> GetCoursesUserHasPurchasedAsync(string userId)
        {
                var userPurchaseBought = await _context.UserCourses
                    .Where(uc => uc.UserId == userId)
                    .Select(uc => uc.CourseId)
                    .ToListAsync();
                return userPurchaseBought;
        }

        [HttpDelete("Delete_AllCourses")]
        public async Task<IActionResult> Delete_AllCourses()
        {
            await _coursesRepository.DeleteAllCoursesAsync();
            return Json(new { status = 200, message = "Delete Courses Successful" });
        }
    }
}
