using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureTasks.Models;
using SecureTasks.ViewModels;

namespace SecureTasks.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly SignInManager<ApplicationUser> _signInMgr;

        public AccountController(UserManager<ApplicationUser> userMgr, SignInManager<ApplicationUser> signInMgr)
        {
            _userMgr = userMgr;
            _signInMgr = signInMgr;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email, FullName = vm.FullName };
            var result = await _userMgr.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                await _userMgr.AddToRoleAsync(user, "User"); // default role
                await _signInMgr.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Dashboard", "User");
            }
            foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _signInMgr.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
                return !string.IsNullOrWhiteSpace(returnUrl) ? LocalRedirect(returnUrl) : RedirectToAction("Dashboard", "User");

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInMgr.SignOutAsync(); // invalidates auth cookie
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied() => Content("Access Denied");
    }
}
