using Microsoft.AspNetCore.Mvc;

namespace WuyiMusic.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Route("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
