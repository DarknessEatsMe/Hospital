using Health.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Health.Controllers
{
    public class AuthController : Controller
    {
        private readonly HealthContext _healthContext;

        public AuthController()
        {
            _healthContext = new HealthContext();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DocPage", "Doctor", new { id = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value) });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Log, Password")] Login login)
        { 
            Login? doc = await _healthContext.Logins.FirstOrDefaultAsync(l => l.Log.Equals(login.Log) & l.Password.Equals(login.Password));
            if (ModelState.IsValid)
            {
                if (doc != null)
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(doc.DocId)),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, "Doctor")
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("DocPage", "Doctor", new {id = doc.DocId});
                }
                ViewData["Info"] = "Не верный логин или пароль";
                return View(login);
            }

            return View(login);
        }
    }
}
