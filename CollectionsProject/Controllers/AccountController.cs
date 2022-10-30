using CollectionsProject.Models.UserModels;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", $"User with {model.Email} not found");
                    return View(model);
                }
                if (user.Status == Status.Blocked)
                {
                    ModelState.AddModelError(string.Empty, $"{user.UserName} has been blocked");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("Password", "Incorrect Password");
            }
            return View(model);
        }

        private static User CreateNewUser(RegisterViewModel regForm)
        {
            User newUser = new()
            {
                Email = regForm.Email,
                UserName = regForm.Name,
                Status = Status.Active,
                Role = Role.User,
            };
            return newUser;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regForm)
        {
            if (ModelState.IsValid)
            {
                User user = CreateNewUser(regForm);
                var result = await _userManager.CreateAsync(user, regForm.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, new List<string> {"User"});
                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(regForm);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
