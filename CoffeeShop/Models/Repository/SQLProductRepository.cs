using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Data;
using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Models.Repository
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly CoffeeShopDbContext _context;
        public SQLProductRepository(CoffeeShopDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<bool> Add(Product product)
        {

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await FindByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Category> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<bool> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
