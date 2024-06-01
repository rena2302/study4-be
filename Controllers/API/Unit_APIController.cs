using Microsoft.AspNetCore.Mvc;
using study4_be.Controllers.Admin;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Services;

namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Unit_APIController : Controller
    {
        private readonly ILogger<Unit_APIController> _logger;
        public UnitRepository _unitRepo = new UnitRepository();
        public Unit_APIController(ILogger<Unit_APIController> logger)
        {
            _logger = logger;
        }
        public STUDY4Context _context = new STUDY4Context();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Get_AllUnitsByCourse")]
        public async Task<ActionResult<IEnumerable<Lesson>>> Get_AllUnitsByCourse(GetAllUnitsByCourses courses)
        {
            var units = await _unitRepo.GetAllUnitsByCourseAsync(courses.courseId);
            return Json(new { status = 200, message = "Get All Units Successful", units });
        }
    }
}
