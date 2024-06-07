using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using study4_be.Models;
using study4_be.Models.ViewModel;
using study4_be.Repositories;

namespace study4_be.Controllers.Admin
{
    public class UnitController : Controller
    {
        private readonly ILogger<UnitController> _logger;
        public UnitController(ILogger<UnitController> logger)
        {
            _logger = logger;
        }
        private readonly UnitRepository _unitsRepository = new UnitRepository();
        public STUDY4Context _context = new STUDY4Context();
        public async Task<IActionResult> Unit_List()
        {
            var units = await _context.Units.ToListAsync();
            return View(units);
        }
        public IActionResult Unit_Create()
        {
            var courses = _context.Courses.ToList();
            var model = new UnitCreateVIewModel
            {
                Units = new Unit(),
                Courses = courses.Select(c => new SelectListItem
                {
                    Value = c.CourseId.ToString(),
                    Text = c.CourseName
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Unit_Create(UnitCreateVIewModel unit)
        {
            if (!ModelState.IsValid)
            {

                return View(unit);
            }
            try
            {
                try
                {
                    await _context.AddAsync(unit);
                    await _context.SaveChangesAsync();
                    unit.Courses = _context.Courses.Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = c.CourseName
                    }).ToList();
                }
                catch (Exception e)
                {
                    CreatedAtAction(nameof(GetUnitById), new { id = unit.Units.UnitId }, unit);
                    _logger.LogError(e, "Error occurred while creating new unit.");
                }
                return RedirectToAction("Index", "Home"); // nav to main home when add successfull, after change nav to index create Courses
            }
            catch (Exception ex)
            {
                // show log
                _logger.LogError(ex, "Error occurred while creating new unit.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                return View(unit);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnitById(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            return Ok(unit);
        }
        public IActionResult Unit_Delete()
        {
            return View();
        }
        public IActionResult Unit_Edit()
        {
            return View();
        }
        public IActionResult Unit_Details()
        {
            return View();
        }
    }
}
