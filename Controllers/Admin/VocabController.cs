using Microsoft.AspNetCore.Mvc;

namespace study4_be.Controllers.Admin
{
    public class VocabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void aa()
        {

        }
    }
}
