using Microsoft.AspNetCore.Mvc;

namespace WuyiMusic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {      
        public IActionResult Index()
        {
            return View();
        }
    }
}
