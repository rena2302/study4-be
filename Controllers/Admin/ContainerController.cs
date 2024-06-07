using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using study4_be.Models;
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
			var containers = await _context.Containers.ToListAsync();
			return View(containers);
		}
		public IActionResult Container_Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Container_Create(Container container)
		{
			if (!ModelState.IsValid)
			{

				return View(container);    //show form with value input and show errors
			}
			try
			{
				try
				{
					await _context.AddAsync(container);
					await _context.SaveChangesAsync();
					CreatedAtAction(nameof(GetContainerById), new { id = container.ContainerId }, container);
				}
				catch (Exception e)
				{
					CreatedAtAction(nameof(GetContainerById), new { id = container.ContainerId }, container);
					_logger.LogError(e, "Error occurred while creating new course.");
				}
				return RedirectToAction("Index", "Home"); // nav to main home when add successfull, after change nav to index create Courses
			}
			catch (Exception ex)
			{
				// show log
				_logger.LogError(ex, "Error occurred while creating new course.");
				ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
				return View(container);
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
