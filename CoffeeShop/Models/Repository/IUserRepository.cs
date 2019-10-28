using CoffeeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Repository
{
    public interface IUserRepository
    {
       Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(int id);
        Task<User> FindByIdAsync(int userId);
        Task<User> CanSignIn(User user);
        Task<IEnumerable<User>> GetAll();
    }
}
