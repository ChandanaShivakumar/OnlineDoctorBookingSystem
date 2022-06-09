using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SprintMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            //Under progress
            return View();
        }
        public IActionResult Login()
        {
            //Login
            return View();
        }
        public IActionResult Register()
        {
            //Create and register User
            return View();
        }
        public IActionResult DoctorList()
        {
            //View DoctorList
            return View();
        }

        public IActionResult DoctorScheduleList()
        {
            //View DoctorScheduleList
            return View();
        }

        public IActionResult BookAppointment()
        {
            //Book Appointment
            return View();
        }

        public IActionResult Patient()
        {
            //Patient Details
            return View();
        }

        public IActionResult Mainn()
        {
            //User Menu
            return View();
        }
        public IActionResult About()
        {
            //About Us
            return View();
        }
        public IActionResult Contact()
        {
            //Contact Us
            return View();
        }
        public IActionResult Privacy()
        {
            //Privacy
            return View();
        }
        public IActionResult Testimonial()
        {
            //Testimonials
            return View();
        }
    }
}
