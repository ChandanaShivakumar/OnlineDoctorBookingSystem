using Microsoft.AspNetCore.Mvc;

namespace SprintMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            return View();
        }

        public IActionResult Patient()
        {
            return View();
        }

        public IActionResult Appointment()
        {
            return View();
        }

        public IActionResult Mainn()
        {
            return View();
        }
        public IActionResult Messages()
        {
            return View();
        }
    }
}
