using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Controllers
{
    public class MenuController : Controller
    {
        private readonly CoffeeShopDbContext _context;

        public MenuController(CoffeeShopDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = _context.Categories.ToListAsync();
            return View(await _context.Categories.ToListAsync());
        }
    }
}