using EmployeeManagmentSystem.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace EmployeeManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserVM model)
        {
            if (ModelState.IsValid) 
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PasswordHash = model.Password; 
                var result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("DashBoard", "Employee");
                }
                else 
                {
                    foreach (var error in result.Errors) 
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View("Register",model);
        }

        [HttpGet]
        public async Task<IActionResult> LogIn()
        {
            return View("LogIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInVM modelLogin)
        {
            if (ModelState.IsValid) 
            {
                ApplicationUser user = await userManager.FindByNameAsync(modelLogin.UserName);
                if (user != null) 
                {
                    bool found = await userManager.CheckPasswordAsync(user, modelLogin.Password);
                    if (found) 
                    {
                        await signInManager.SignInAsync(user,modelLogin.RemmemberMe);
                        return RedirectToAction("DashBoard", "Employee");
                    }
                    return View("LogIn", modelLogin);
                }
                ModelState.AddModelError("","UserName or PassWord is Wrong ");
            }
            return View("LogIn",modelLogin);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return View("LogIn");
        }

    }

}
