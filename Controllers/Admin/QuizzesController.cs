using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;

namespace study4_be.Controllers.Admin
{
    public class QuizzesController : Controller
    {
        private readonly ILogger<QuizzesController> _logger;
        public QuizzesController(ILogger<QuizzesController> logger)
        {
            _logger = logger;
        }
        private readonly QuizzRepository _quizzesRepository = new QuizzRepository();
        public STUDY4Context _context = new STUDY4Context();

        [HttpGet("GetAllQuizzes")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllQuizzes()
        {
            var quizzes = await _quizzesRepository.GetAllQuizzesAsync();
            return Json(new { status = 200, message = "Get Quizzes Successful", quizzes });

        }
        //development enviroment
        [HttpDelete("DeleteAllQuizzes")]
        public async Task<IActionResult> DeleteAllQuizzes()
        {
            await _quizzesRepository.DeleteAllQuizzesAsync();
            return Json(new { status = 200, message = "Delete Quizzes Successful" });
        }
        public IActionResult Quizzes_List()
        {
            return View();
        }
        public IActionResult Quizzes_Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Quizzes_Create(Quiz quiz)
        {
            if (!ModelState.IsValid)
            {

                return View(quiz);    //show form with value input and show errors
            }
            try
            {
                try
                {
                    await _context.AddAsync(quiz);
                    await _context.SaveChangesAsync();
                    CreatedAtAction(nameof(GetQuizzById), new { id = quiz.QuizzesId }, quiz);
                }
                catch (Exception e)
                {
                    CreatedAtAction(nameof(GetQuizzById), new { id = quiz.QuizzesId }, quiz);
                    _logger.LogError(e, "Error occurred while creating new quiz.");
                }
                return RedirectToAction("Index", "Home"); // nav to main home when add successfull, after change nav to index create Courses
            }
            catch (Exception ex)
            {
                // show log
                _logger.LogError(ex, "Error occurred while creating new quiz.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                return View(quiz);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizzById(int id)
        {
            var quizz = await _context.Quizzes.FindAsync(id);
            if (quizz == null)
            {
                return NotFound();
            }

            return Ok(quizz);
        }
        public IActionResult Quizzes_Delete()
        {
            return View();
        }
        public IActionResult Quizzes_Edit()
        {
            return View();
        }
        public IActionResult Quizzes_Details()
        {
            return View();
        }
    }
}
