using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.MenuViewModels;
using CoffeeShop.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Controllers
{
    public class MenuController : Controller
    {
        private readonly SQLCategoryRepository _categoryRepository;
        private readonly SQLProductRepository _productRepository;

        public MenuController(SQLCategoryRepository categoryRepository, SQLProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}