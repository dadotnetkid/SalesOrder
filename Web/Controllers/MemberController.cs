using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Data.Models;
using Services.VM;

namespace Web.Controllers
{
    public class MemberController : Controller
    {
        private UserManager<IdentityUser> userManager;


        private SignInManager<IdentityUser> signInManager;

        public MemberController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("UserName", "Invalid Email Address");
                    return View(model);
                }
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (result.Succeeded)
                    return RedirectToAction("index", "home");
                else
                {
                    ModelState.AddModelError("Password", "Invalid Password");
                    return View(model);

                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var user = new IdentityUser()
                {
                    UserName = model.UserName,
                    Email = model.UserName,

                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login");
            }
            catch (Exception e)
            {

            }
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Login");

        }
    }
}
