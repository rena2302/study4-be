using Microsoft.AspNetCore.Mvc;
using study4_be.Models;

namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCourses_APIController : Controller
    {
        public STUDY4Context _context = new STUDY4Context();

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Get_AllUserCourses([FromBody] String userId)
        {
            return View(userId);
        } 
    }
}
