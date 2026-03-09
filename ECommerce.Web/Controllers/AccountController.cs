using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace ECommerce.Web.Controllers
{
    /// <summary>
    /// Controller for handling admin authentication.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <returns>The login view.</returns>
        [HttpGet]
        public IActionResult Login() => View();

        /// <summary>
        /// Processes a login request.
        /// </summary>
        /// <param name="username">Entered username.</param>
        /// <param name="password">Entered password.</param>
        /// <returns>Redirects to catalog on success; returns to view on failure.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "password") // Simple POC creds
            {
                var claims = new List<System.Security.Claims.Claim> { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "Admin") };
                var identity = new System.Security.Claims.ClaimsIdentity(claims, "CookieAuth");
                await HttpContext.SignInAsync("CookieAuth", new System.Security.Claims.ClaimsPrincipal(identity));
                return RedirectToAction("Index", "Catalog");
            }
            ViewBag.Error = "Invalid credentials";
            return View();
        }

        /// <summary>
        /// Processes a logout request.
        /// </summary>
        /// <returns>Redirects to the homepage.</returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Catalog");
        }
    }
}
