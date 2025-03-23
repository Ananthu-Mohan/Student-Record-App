using Microsoft.AspNetCore.Mvc;

namespace StudentsRecordApplication.Controllers
{
    public class StartupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
