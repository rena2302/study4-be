using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Repositories;

namespace study4_be.Controllers.Admin
{
    public class TranslateController : Controller
    {
        private readonly ILogger<TranslateController> _logger;
        public TranslateController(ILogger<TranslateController> logger)
        {
            _logger = logger;
        }
        private readonly AudioRepository _audiosRepository = new AudioRepository();
        public STUDY4Context _context = new STUDY4Context();
        [HttpGet("GetAllTransaltes")]
        public async Task<ActionResult<IEnumerable<Translate>>> GetAllTransaltes()
        {
            var audios = await _audiosRepository.GetAllAudiosAsync();
            return Json(new { status = 200, message = "Get Transalte Successful", audios });

        }
        [HttpDelete("DeleteAllTransaltes")]
        public async Task<IActionResult> DeleteAlTransaltes()
        {
            await _audiosRepository.DeleteAllAudiosAsync();
            return Json(new { status = 200, message = "Delete Transalte Successful" });
        }
        public IActionResult Translate_List()
        {
            return View();
        }
        public IActionResult Translate_Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Translate_Create(Translate translate)
        {
            if (!ModelState.IsValid)
            {

                return View(translate);    //show form with value input and show errors
            }
            try
            {
                try
                {
                    await _context.AddAsync(translate);
                    await _context.SaveChangesAsync();
                    CreatedAtAction(nameof(GetTranslateById), new { id = translate.TranslateId }, translate);
                }
                catch (Exception e)
                {
                    CreatedAtAction(nameof(GetTranslateById), new { id = translate.TranslateId }, translate);
                    _logger.LogError(e, "Error occurred while creating new translate.");
                }
                return RedirectToAction("Index", "Home"); // nav to main home when add successfull, after change nav to index create Courses
            }
            catch (Exception ex)
            {
                // show log
                _logger.LogError(ex, "Error occurred while creating new translate.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                return View(translate);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTranslateById(int id)
        {
            var translate = await _context.Translates.FindAsync(id);
            if (translate == null)
            {
                return NotFound();
            }

            return Ok(translate);
        }

        public IActionResult Translate_Delete()
        {
            return View();
        }
        public IActionResult Translate_Edit()
        {
            return View();
        }
        public IActionResult Translate_Details()
        {
            return View();
        }
    }
}
