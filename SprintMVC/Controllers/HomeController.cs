using Microsoft.AspNetCore.Mvc;

namespace SprintMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
