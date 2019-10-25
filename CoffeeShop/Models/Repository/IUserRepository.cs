using CoffeeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUser(int Id);
        Task<IEnumerable<User>> GetAllUser();
        Task<User> Add(User user);
        Task<User> Update(User userChanges);
        Task<User> Delete(int Id);
        Task<User> CanSignin(string user, string password);
    }
}
