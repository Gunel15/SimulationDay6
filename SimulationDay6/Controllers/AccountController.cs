using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimulationDay6.Models;
using SimulationDay6.ViewModels.Account;

namespace SimulationDay6.Controllers
{
    public class AccountController(UserManager<User> _userManager,RoleManager<IdentityRole> _roleManager,SignInManager<User> _signInManager): Controller
    {
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            User user = new()
            {
                FullName = vm.FullName,
                UserName = vm.Username,
                Email = vm.Email,
            };
            var result= await _userManager.CreateAsync(user,vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> CreateRoles()
        {

            await _roleManager.CreateAsync(new() { Name = "Admin" });
            await _roleManager.CreateAsync(new() { Name = "Member" });
            await _roleManager.CreateAsync(new() { Name = "Nazarata nazarat" });
        }

    }
}
