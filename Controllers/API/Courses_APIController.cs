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

        [HttpGet("GetAllCourses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            var courses = await _coursesRepository.GetAllCoursesAsync();
            return Json(new { status = 200, message = "Get Courses Successful", courses });

        }
        //development enviroment
        [HttpDelete("DeleteAllCourses")]
        public async Task<IActionResult> DeleteAllCourses()
        {
            await _coursesRepository.DeleteAllCoursesAsync();
            return Json(new { status = 200, message = "Delete Courses Successful" });
        }
        [HttpPost("Buy_Course")] // thieu number phone
        public async Task<IActionResult> Buy_Course([FromBody] BuyCourseRequest request)
        {
            if (request == null || request.UsersId == null || request.CourseId == null)
            {
                return BadRequest("Invalid user or course information.");
            }

            var existingUser = await _context.Users.FindAsync(request.UsersId);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }
            var existingOrder = await _context.Orders
               .FirstOrDefaultAsync(o => o.UsersId == request.UsersId && o.CourseId == request.CourseId);

            if (existingOrder != null)
            {
                return BadRequest("You have already ordered this course.");
            }

            var existingCourse = await _context.Courses.FindAsync(request.CourseId);
            if (existingCourse == null)
            {
                return NotFound("Course not found.");
            }
            var order = new Order
            {
                UsersId = existingUser.UserId,
                CourseId = existingCourse.CourseId,
                OrderDate = DateTime.Now
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok("Course purchased successfully.");
        }
        [HttpPost("Delete_AllCourse")] // thieu number phone
        public async Task<IActionResult> Delete_AllCourse([FromBody] BuyCourseRequest request)
        {
            if (request == null || request.UsersId == null || request.CourseId == null)
            {
                return BadRequest("Invalid user or course information.");
            }

            var existingUser = await _context.Users.FindAsync(request.UsersId);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }
            var existingOrder = await _context.Orders
               .FirstOrDefaultAsync(o => o.UsersId == request.UsersId && o.CourseId == request.CourseId);

            if (existingOrder != null)
            {
                return BadRequest("You have already ordered this course.");
            }

            var existingCourse = await _context.Courses.FindAsync(request.CourseId);
            if (existingCourse == null)
            {
                return NotFound("Course not found.");
            }
            var order = new Order
            {
                UsersId = existingUser.UserId,
                CourseId = existingCourse.CourseId,
                OrderDate = DateTime.Now
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok("Course purchased successfully.");
        }
    }
}
