using CoffeeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> FindByIdAsync(int id);
        Task<bool> Add(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(int id);
    }
}
