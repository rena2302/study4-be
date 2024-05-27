using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;

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
    }
}
