using EmployeeManagmentSystem.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace EmployeeManagmentSystem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AdminController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(AdminVM adminModel) 
        {
            if (ModelState.IsValid) 
            {
               ApplicationUser admin = new ApplicationUser();
                admin.UserName = adminModel.UserName;
                admin.PasswordHash = adminModel.Password;
                admin.Email = adminModel.Email;
                var result = await userManager.CreateAsync(admin,adminModel.Password);
                if (result.Succeeded) 
                {
                    var addrole = await userManager.AddToRoleAsync(admin, "Admin");
                    if (addrole.Succeeded)
                    {
                        await signInManager.SignInAsync(admin,false);
                        return RedirectToAction("DashBoard", "Employee");
                    }
                }
                foreach (var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("RegisterAdmin",adminModel);
        }
    }
}
