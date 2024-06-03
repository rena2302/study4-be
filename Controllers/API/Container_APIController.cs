using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Services.Request;
using study4_be.Services.Response;

namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Container_APIController : Controller
    {
        private readonly ILogger<Container_APIController> _logger;
        public ContainerRepository _containerRepo = new ContainerRepository();
        public Container_APIController(ILogger<Container_APIController> logger)
        {
            _logger = logger;
        }
        public STUDY4Context _context = new STUDY4Context();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Get_AllContainerAndLessonByUnit")]
        public async Task<ActionResult<UnitDetailResponse>> Get_AllContainerAndLessonByUnit(GetAllContainerAndLessionRequestcs unit)
        {
            using (var context = new STUDY4Context())
            {
                //Get the list of containers based on unitId
                var containers = await context.Containers
                                              .Where(c => c.UnitId == unit.unitId)
                                              .ToListAsync();
                var unitExist = await context.Units.FindAsync(unit.unitId);
                // Initialize an empty list to hold all lessons
                var lessons = new List<Lesson>();

                // Loop through each container and get its lessons
                foreach (var container in containers)
                {
                    var containerLessons = await context.Lessons
                                                        .Where(l => l.ContainerId == container.ContainerId)
                                                        .ToListAsync();
                    lessons.AddRange(containerLessons);
                }

                // Return both lists in a UnitDetail object
                var unitDetail = new UnitDetailResponse
                {
                    unitId = unit.unitId,
                    unitName = unitExist.UnitTittle,
                    Containers = containers,
                };

                return Ok(new { status = 200, message = "Get All Units and Containers Successful", unitDetail });
            }
        }
    }
}
