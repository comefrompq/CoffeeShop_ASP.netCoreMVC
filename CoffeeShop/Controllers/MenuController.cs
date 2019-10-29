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
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public MenuController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = new MenuViewModel
            {
                categories = await _categoryRepository.GetAll(),
                products = (await _categoryRepository.FindByIdAsync(1)).Products
            };
            return View(model);
        }
        public async Task<IActionResult> ProductList(int id)
        {
            var model = new MenuViewModel
            {
                categories = await _categoryRepository.GetAll(),
                products = (await _categoryRepository.FindByIdAsync(id)).Products
            };
            return View("Index",model);
        }
        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var model = new ProductDetailViewModel
            {
                product = await _productRepository.FindByIdAsync(id)
            };
            return View(model);
        }
    }
}