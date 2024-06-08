using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using study4_be.Models;
using study4_be.Models.ViewModel;
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
        private readonly VocabRepository _vocabsRepository = new VocabRepository();
        public STUDY4Context _context = new STUDY4Context();
        [HttpGet("GetAllVocabs")]
        public async Task<ActionResult<IEnumerable<Vocabulary>>> GetAllVocabs()
        {
            var vocabs = await _vocabsRepository.GetAllVocabAsync();
            return Json(new { status = 200, message = "Get Vocab Successful", vocabs });

        }

        [HttpDelete("DeleteAllVocabs")]
        public async Task<IActionResult> DeleteAllVocabs()
        {
            await _vocabsRepository.DeleteAllVocabAsync();
            return Json(new { status = 200, message = "Delete Vocab Successful" });
        }
        public async Task<IActionResult> Vocab_List()
        {
            var vocabs = await _vocabsRepository.GetAllVocabAsync(); // Retrieve list of courses from repository
            return View(vocabs); // Pass the list of courses to the view
        }
        public IActionResult Vocab_Create()
        {
            var lessons = _context.Lessons.ToList();
            var model = new VocabCreateViewModel
            {
                vocab = new Vocabulary(),
                lesson = lessons.Select(c => new SelectListItem
                {
                    Value = c.LessonId.ToString(),
                    Text = c.LessonTitle.ToString()
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Vocab_Create(VocabCreateViewModel vocabViewModel)
        {
            try
            {
                var vocabulary = new Vocabulary
                {
                    VocabId = vocabViewModel.vocab.VocabId,
                    VocabType = vocabViewModel.vocab.VocabType,
                    AudioUrlUk = vocabViewModel.vocab.AudioUrlUk,
                    AudioUrlUs = vocabViewModel.vocab.AudioUrlUs,
                    Mean = vocabViewModel.vocab.Mean,
                    Example = vocabViewModel.vocab.Example,
                    Explanation = vocabViewModel.vocab.Explanation,
                    LessonId = vocabViewModel.vocab.LessonId,
                };

                await _context.AddAsync(vocabulary);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating new unit.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");

                vocabViewModel.lesson = _context.Lessons.Select(c => new SelectListItem
                {
                    Value = c.LessonId.ToString(),
                    Text = c.LessonTitle.ToString()
                }).ToList();

                return View(vocabViewModel);
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
