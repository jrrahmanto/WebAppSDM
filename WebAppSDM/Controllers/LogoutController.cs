using Microsoft.AspNetCore.Mvc;

namespace WebAppSDM.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Login");
        }
    }
}
