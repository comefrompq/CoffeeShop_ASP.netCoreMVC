using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoffeeShop.Data.Models;
using CoffeeShop.Models.AccountViewModels;
using CoffeeShop.Models.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel form)
        {
            var res = await _userRepository.CanSignin(form.UserName, form.Password);
            if(res == null)
            {
                return View();

            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, form.UserName),
                new Claim(ClaimTypes.Role,res.Role.Name)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", "Users");

        }
        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel form)
        {
            User user = new User 
            { UserName = form.UserName
            , Password = form.Password
            , RoleId = 2
            , FullName = form.UserName};
            await _userRepository.Add(user);
            return RedirectToAction("Login", "Account");
        }


    }
}