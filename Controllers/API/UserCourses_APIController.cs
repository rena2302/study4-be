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
            var courses = await _userCoursesRepo.Get_AllCoursesByUser(request.userId);
            return Json(new { status = 200, message = "Get All Courses By User Successful", courses });
        }
        [HttpPost("Get_AllUsersBuyCourse")]
        public async Task<ActionResult<IEnumerable<User>>> Get_AllUsersBuyCourse(GetAllUsersBuyCourse request)
        {
            var userList = await _userCoursesRepo.Get_AllUserBuyCourses(request.courseId);
            var totalAmount =  userList.Count();
            return Json(new { status = 200, message = "Get All User Buy Courses Successful", userList, totalAmount });
        }
        [HttpGet("Get_AllUserCourses")]
        public async Task<ActionResult<IEnumerable<User>>> Get_AllUserCourses()
        {
            var courses = await _userCoursesRepo.GetAllUserCoursesAsync();
            return Json(new { status = 200, message = "Get All UserCourses Successful", courses });
        }
    }
}
