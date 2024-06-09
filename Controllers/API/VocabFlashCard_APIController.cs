using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Services.Request;
using study4_be.Services.Response;
using Microsoft.Extensions.Logging;

namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabFlashCard_APIController : Controller
    {
        private readonly STUDY4Context _context;
        private readonly VocabFlashCardRepository _vocabFlashCardRepo;
        private readonly ILogger<VocabFlashCard_APIController> _logger;
        public VocabFlashCard_APIController(ILogger<VocabFlashCard_APIController> logger) 
        {
            _context = new STUDY4Context();
            _vocabFlashCardRepo = new VocabFlashCardRepository();
            _logger = logger; 
        }
        [HttpPost("Get_AllVocabOfLesson")]
        public async Task<IActionResult> Get_AllVocabOfLesson([FromBody] VocabFlashCardRequest _vocabRequest) {
            if (_vocabRequest.lessonId == null)
            {
                _logger.LogWarning("LessonId is null or empty in the request.");
                return BadRequest(new { status = 400, message = "LessonId is null or empty" });
            }

            try
            {
                var allVocabOfLesson = await _vocabFlashCardRepo.GetAllVocabDependLesson(_vocabRequest.lessonId);
                return Ok(new { status = 200, message = "Get All Vocab Of Lesson Successful", data = allVocabOfLesson });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching vocab for lesson {LessonId}", _vocabRequest.lessonId);
                return StatusCode(500, new { status = 500, message = "An error occurred while processing your request." });
            }
        }
        [HttpPost("Get_AllVocabFindpair")]
        public async Task<IActionResult> Get_AllVocabFindpair([FromBody] VocabFlashCardRequest _vocabRequest)
        {
            if (_vocabRequest.lessonId == null)
            {
                _logger.LogWarning("LessonId is null or empty in the request.");
                return BadRequest(new { status = 400, message = "LessonId is null or empty" });
            }

            try
            {
                var allVocabOfLesson = await _vocabFlashCardRepo.GetAllVocabDependLesson(_vocabRequest.lessonId);

                var responseData = allVocabOfLesson.Select(vocab => new VocabFindPairResponse
                {
                    vocabId = vocab.VocabId,
                    vocabMean = vocab.Mean,
                    vocabExplanation = vocab.Explanation,
                    vocabTitle = vocab.VocabTitle
                }).ToList();
                return Ok(new { status = 200, message = "Get All Vocab Of Lesson Successful", data = responseData });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching vocab for lesson {LessonId}", _vocabRequest.lessonId);
                return StatusCode(500, new { status = 500, message = "An error occurred while processing your request." });
            }
        }


    }
}
