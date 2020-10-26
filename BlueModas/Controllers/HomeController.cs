using Microsoft.AspNetCore.Mvc;

namespace BlueModas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shopping()
        {
            return View();
        }
    }
}
