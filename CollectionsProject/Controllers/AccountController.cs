using CollectionsProject.Models.UserModels;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CollectionsProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IStringLocalizer<AccountController> _Loc;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IStringLocalizer<AccountController> loc)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Loc = loc;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated) //if user authenticated - no reason for register or login
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
                if (user == null) //no such user
                {
                    ModelState.AddModelError("Email", $"{_Loc["User with"]} {model.Email} {_Loc["not found"]}");
                    return View(model);
                }
                if (user.Status == Status.Blocked)//if user is blocked
                {
                    ModelState.AddModelError(string.Empty, $"{user.UserName} {_Loc["has been blocked"]}");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded) //success
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("Password", _Loc["Incorrect Password"]); //otherwise - incorrect password
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
                if (result.Succeeded) //success
                {
                    await _userManager.AddToRolesAsync(user, new List<string> {"User"});
                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors) //errors (email or username exists)
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
