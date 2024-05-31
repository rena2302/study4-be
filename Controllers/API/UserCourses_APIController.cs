using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Services;

namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCourses_APIController : Controller
    {
        public STUDY4Context _context = new STUDY4Context();
        public UserCoursesRepository _userCoursesRepo = new UserCoursesRepository();

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Get_AllCoursesByUser")]
        public async Task<ActionResult<IEnumerable<User>>> Get_AllCoursesByUser(GetAllCoursesByUserRequest request)
        {
            //return Json(new { status = 200, message = "Get All UserCourses Successful", request });
            var courses = await _userCoursesRepo.Get_AllCoursesByUser(request.userId);
            return Json(new { status = 200, message = "Get All Courses By User Successful", courses });
        }
        [HttpGet("Get_AllUserCourses")]
        public async Task<ActionResult<IEnumerable<User>>> Get_AllUserCourses()
        {
            var courses = await _userCoursesRepo.GetAllUserCoursesAsync();
            return Json(new { status = 200, message = "Get All UserCourses Successful", courses });
        }
    }
}
