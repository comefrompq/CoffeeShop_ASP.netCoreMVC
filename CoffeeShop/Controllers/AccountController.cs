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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public AccountController(IUserRepository userRepository, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
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
            User user = new User
            {
                UserName = form.UserName,
                Password = form.Password
            };
            var res = await _userRepository.CanSignIn(user);
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
            if(res.Role.Name == "admin")
            {
                return RedirectToAction("UserManagement", "Account");
            }
            return RedirectToAction("Index", "Menu");

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
            , FullName = form.UserName};
            await _userRepository.Add(user);
            return RedirectToAction("Login", "Account");
        }
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> UserManagement()
        {
            var model = new ManagementViewModel
            {
                users = await _userRepository.GetAll(),
            };
            return  View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CategoryManagement()
        {
            var model = new ManagementViewModel
            {
                categories = await _categoryRepository.GetAll()
            };
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ProductManagement()
        {
            var model = new ManagementViewModel
            {
                products = await _productRepository.GetAll()
            };
            return View(model);
        }
    }
}