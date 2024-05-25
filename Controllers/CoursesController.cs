using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using study4_be.Models;
using study4_be.Repositories;
using Microsoft.Extensions.Logging;
namespace study4_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly ILogger<CoursesController> _logger;
        public CoursesController(ILogger<CoursesController> logger) 
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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowCourseList()
        {
            return View();
        }
        public IActionResult Course_List()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Course_Create()
        {
            return View();
        }
        [HttpPost("Course_Create")]
        public async Task<IActionResult> Course_Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                
                return View(course);    //show form with value input and show errors
            }
            try
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // nav to main home when add successfull, after change nav to index create Courses
            }
            catch (Exception ex)
            {
                // show log
                _logger.LogError(ex, "Error occurred while creating new course.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                return View(course);
            }
        }
        public IActionResult Course_Edit()
        {
            return View();
        }

        public IActionResult Course_Delete()
        {
            return View();
        }

        public IActionResult Course_Details()
        {
            return View();
        }
    }
}
