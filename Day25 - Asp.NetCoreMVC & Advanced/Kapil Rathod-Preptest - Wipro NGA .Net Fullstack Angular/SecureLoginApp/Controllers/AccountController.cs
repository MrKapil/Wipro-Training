using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SecureLoginApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;

namespace SecureLoginApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataProtector _protector;

        public AccountController(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 IDataProtectionProvider dataProtectionProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _protector = dataProtectionProvider.CreateProtector("SecureLoginApp.SampleProtector");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null) return RedirectToAction("Index", "Home");

                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    TempData["Message"] = "Welcome, Admin! You have access to the Admin Dashboard.";
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (await _userManager.IsInRoleAsync(user, "User"))
                {
                    TempData["Message"] = "Welcome, User! Here is your profile information.";
                    return RedirectToAction("UserProfile", "Account");
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LoggedOut", "Home");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

        
            var protectedSample = _protector.Protect($"Profile:{user.UserName}:{DateTime.UtcNow}");

            var model = new UserProfileViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Message = $"(a demo protected token was created for you: {protectedSample.Substring(0, Math.Min(30, protectedSample.Length))}...)"
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
