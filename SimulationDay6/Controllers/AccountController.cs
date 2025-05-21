using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimulationDay6.Models;
using SimulationDay6.ViewModels.Account;

namespace SimulationDay6.Controllers
{
    public class AccountController(UserManager<User> _userManager,RoleManager<IdentityRole> _roleManager,SignInManager<User> _signInManager): Controller
    {
        public async Task<IActionResult> CreateRoles()
        {

            await _roleManager.CreateAsync(new() { Name = "Admin" });
            await _roleManager.CreateAsync(new() { Name = "Member" });
            await _roleManager.CreateAsync(new() { Name = "Nazarata nazarat" });
            return Ok("Rollar yarandi");
        }

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
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var user=await _userManager.FindByEmailAsync(vm.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "Username or password is wrong!");
                    return View(vm);
            }

            var result=await _signInManager.PasswordSignInAsync(user,vm.Password,isPersistent:false,lockoutOnFailure:false);   
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is wrong!");
                    return View(vm);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateAdmin()
        {
            User admin = new()
            {
                FullName = "Admin",
                UserName = "admin",
                Email = "admin@gmail.com"
            };
            await _userManager.CreateAsync(admin,"Admin");
            await _userManager.AddToRoleAsync(admin, "Admin123@");
            return Ok("Created admin");
        }


    }
}
