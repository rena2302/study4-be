using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using study4_be.Models;
using study4_be.Models.ViewModel;
using study4_be.Repositories;

namespace study4_be.Controllers.Admin
{
	public class ContainerController : Controller
	{
		private readonly ILogger<ContainerController> _logger;
		public ContainerController(ILogger<ContainerController> logger)
		{
			_logger = logger;
		}
		private readonly ContainerRepository _containersRepository = new ContainerRepository();
		public STUDY4Context _context = new STUDY4Context();
		public async Task<IActionResult> Container_List()
		{
            var containers = await _context.Containers
                   .Include(c => c.Unit)
                       .ThenInclude(u => u.Course)
                   .ToListAsync();

            var containerViewModels = containers.Select(container => new ContainerListViewModel
            {
                container = container,
                courseName = $"{container.Unit.Course.CourseName}",
                unitTitle = container.Unit.UnitTittle // Assuming UnitTittle is the property for the unit title
            });

            return View(containerViewModels);
        }
		public IActionResult Container_Create()
		{
            var units = _context.Units.Include(u => u.Course).ToList();
            var model = new ContainerCreateViewModel
            {
                containers = new Container(),
                units = units.Select(c => new SelectListItem
                {
                    Value = c.UnitId.ToString(),
                    Text = c.UnitTittle + " : " + c.Course.CourseName
                }).ToList()
            };
            return View(model);
		}
		[HttpPost]
        public async Task<IActionResult> Container_Create(ContainerCreateViewModel containerViewModel)
        {
            try
            {
                var container = new Container
                {
                    ContainerId = containerViewModel.containers.ContainerId,
                    ContainerTitle = containerViewModel.containers.ContainerTitle,
                    UnitId = containerViewModel.containers.UnitId,
                };

                await _context.AddAsync(container);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating new unit.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");

                containerViewModel.units = _context.Units.Select(c => new SelectListItem
                {
                    Value = c.UnitId.ToString(),
                    Text = c.UnitTittle + " : " + c.Course.CourseName
                }).ToList();
                return View(containerViewModel);
            }
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> GetContainerById(int id)
		{
			var container = await _context.Containers.FindAsync(id);
			if (container == null)
			{
				return NotFound();
			}

			return Ok(container);
		}

		public IActionResult Container_Edit()
		{
			return View();
		}

		public IActionResult Container_Delete()
		{
			return View();
		}

		public IActionResult Container_Details()
		{
			return View();
		}
	}
}
