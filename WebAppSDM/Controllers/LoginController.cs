using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDBContext _context;
        public LoginController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HttpContext.Session.SetString("NotificationLogin", "");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Auth(Login dt)
        {
            try
            {
                var db = _context.AUser.Where(x => x.isactive == 1 && x.username == dt.username && x.password == dt.password).FirstOrDefault();
                if (db == null)
                {
                    HttpContext.Session.SetString("NotificationLogin", "Incorrect password or username !");
                    return View("Index");
                }
                else
                {
                    var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, db.username)
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    HttpContext.Session.SetString("nip", db.nip);
                    return RedirectToAction("Index", "Home");
                }
            
            }
            catch (Exception x)
            {
                HttpContext.Session.SetString("NotificationLogin", x.Message); 
                throw;
            }
        }
    }
}
