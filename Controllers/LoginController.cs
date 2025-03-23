using Microsoft.AspNetCore.Mvc;

namespace StudentsRecordApplication.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GotoDashboard()
        {
            return RedirectToAction("Index","Home");
        }
    }
}
