using CoffeeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<bool> Add(Product product);
        Task<bool> Delete(int id);
        Task<bool> Update(Product product);
        Task<Product> FindByIdAsync(int id);
        Task<Category> GetByCategoryIdAsync(int categoryId);
    }
}
