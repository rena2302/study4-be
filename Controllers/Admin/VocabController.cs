using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;

namespace study4_be.Controllers.Admin
{
    public class VocabController : Controller
    {
        private readonly ILogger<VocabController> _logger;
        public VocabController(ILogger<VocabController> logger)
        {
            _logger = logger;
        }
        private readonly LessonRepository _lessonsRepository = new LessonRepository();
        public STUDY4Context _context = new STUDY4Context();
        [HttpGet("GetAllVocabs")]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetAllVocabs()
        {
            var lessons = await _lessonsRepository.GetAllLessonsAsync();
            return Json(new { status = 200, message = "Get Vocab Successful", lessons });

        }

        [HttpDelete("DeleteAllVocabs")]
        public async Task<IActionResult> DeleteAllVocabs()
        {
            await _lessonsRepository.DeleteAllLessonsAsync();
            return Json(new { status = 200, message = "Delete Vocab Successful" });
        }
        public IActionResult Vocab_List()
        {
            return View();
        }
        public IActionResult Vocab_Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Vocab_Create(Vocabulary vocabulary)
        {
            if (!ModelState.IsValid)
            {

                return View(vocabulary);    //show form with value input and show errors
            }
            try
            {
                try
                {
                    await _context.AddAsync(vocabulary);
                    await _context.SaveChangesAsync();
                    CreatedAtAction(nameof(GetVocabById), new { id = vocabulary.VocabId }, vocabulary);
                }
                catch (Exception e)
                {
                    CreatedAtAction(nameof(GetVocabById), new { id = vocabulary.VocabId }, vocabulary);
                    _logger.LogError(e, "Error occurred while creating new vocabulary.");
                }
                return RedirectToAction("Index", "Home"); // nav to main home when add successfull, after change nav to index create Courses
            }
            catch (Exception ex)
            {
                // show log
                _logger.LogError(ex, "Error occurred while creating new vocabulary.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                return View(vocabulary);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVocabById(int id)
        {
            var vocab = await _context.Vocabularies.FindAsync(id);
            if (vocab == null)
            {
                return NotFound();
            }

            return Ok(vocab);
        }

        public IActionResult Vocab_Delete()
        {
            return View();
        }
        public IActionResult Vocab_Edit()
        {
            return View();
        }
        public IActionResult Vocab_Details()
        {
            return View();
        }
    }
}
