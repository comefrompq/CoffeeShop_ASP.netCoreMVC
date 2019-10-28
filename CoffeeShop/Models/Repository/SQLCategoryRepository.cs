using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Data;
using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Models.Repository
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly CoffeeShopDbContext _context;
        public SQLCategoryRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await FindByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<bool> Update(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
