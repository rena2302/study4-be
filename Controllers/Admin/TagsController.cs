using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using study4_be.Models;
using study4_be.Repositories;

namespace study4_be.Controllers.Admin
{
    public class TagsController : Controller
    {
        private readonly ILogger<TagsController> _logger;
        public TagsController(ILogger<TagsController> logger)
        {
            _logger = logger;
        }
        public STUDY4Context _context = new STUDY4Context();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Tag_List()
        {
            var tags = await _context.Tags.ToListAsync(); // Retrieve list of courses from repository
            return View(tags); // Pass the list of courses to the view
        }
        public IActionResult Tag_Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Tag_Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {

                return View(tag);    //show form with value input and show errors
            }
            try
            {
                try
                {
                    await _context.AddAsync(tag);
                    await _context.SaveChangesAsync();
                    CreatedAtAction(nameof(GetTagById), new { id = tag.TagId }, tag);
                }
                catch (Exception e)
                {
                    CreatedAtAction(nameof(GetTagById), new { id = tag.TagId }, tag);
                    _logger.LogError(e, "Error occurred while creating new course.");
                }
                return RedirectToAction("Index", "Home"); // nav to main home when add successfull, after change nav to index create Courses
            }
            catch (Exception ex)
            {
                // show log
                _logger.LogError(ex, "Error occurred while creating new course.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                return View(tag);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        public IActionResult Tag_Edit()
        {
            return View();
        }

        public IActionResult Tag_Delete()
        {
            return View();
        }

        public IActionResult Tag_Details()
        {
            return View();
        }
    }
}
