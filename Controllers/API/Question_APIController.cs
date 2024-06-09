using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Services.Request;

namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Question_APIController : Controller
    {
        public STUDY4Context _context;
        public QuestionRepository _questionRepo;
        private readonly ILogger<Question_APIController> _logger;
        public Question_APIController(ILogger<Question_APIController> logger)
        {
            _questionRepo = new QuestionRepository();
            _context = new STUDY4Context();
            _logger = logger;
        }

        [HttpPost("Get_AllQuestionOfLesson")]
        public async Task<IActionResult> Get_AllQuestionOfLesson(QuestionRequest _questionRequest)
        {
            if (_questionRequest.lessonId == null)
            {
                _logger.LogWarning("LessonId is null or empty in the request.");
                return BadRequest(new { status = 400, message = "LessonId is null or empty" });
            }

            try
            {
                var allQuestionOfLesson = await _questionRepo.GetAllQuestionsOfLesson(_questionRequest.lessonId);
                return Json(new { status = 200, message = "Get All Question Of Lesson Successful", allQuestionOfLesson });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching vocab for lesson {LessonId}", _questionRequest.lessonId);
                return StatusCode(500, new { status = 500, message = "An error occurred while processing your request." });
            }
        }
    }
}
