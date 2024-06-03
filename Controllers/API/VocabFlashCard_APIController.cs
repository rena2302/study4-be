using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Services.Request;
using study4_be.Services.Response;

namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabFlashCard_APIController : Controller
    {
        public STUDY4Context _context = new STUDY4Context();
        public VocabFlashCardRepository _vocabFlashCardRepo = new VocabFlashCardRepository();

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Get_AllVocabOfLesson")]
        public async Task<IActionResult> Get_AllVocabOfLesson(VocabFlashCardRequest _vocabRequest) {
            var allVocabOfLesson = await _vocabFlashCardRepo.GetAllVocabDependLesson(_vocabRequest.lessonId);
            return Json(new { status = 200, message = "Get All Vocab Of Lesson Successful", allVocabOfLesson });
        }

    }
}
