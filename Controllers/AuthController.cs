using MESSAGENOID.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MESSAGENOID.Models.ViewModels;

namespace MESSAGENOID.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Auth/Register.cshtml");
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);

            // The 'PublicKey' comes from a hidden input populated by JS
            var result = await _authService.RegisterAsync(model.Email, model.Password, model.userName, model.PublicKey);

            if (result.Succeeded)
            {
                // Log them in immediately after registration
                await _authService.LoginAsync(model.Email, model.Password, false);
                return RedirectToAction("Index", "Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // GET: /Account/Login
        [HttpGet]
        public  IActionResult Login()
        {
            return View("~/Views/Auth/Login.cshtml");
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _authService.LoginAsync(model.email, model.passWord, model.rememberMe);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Profile");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Account locked due to too many failed attempts.");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
