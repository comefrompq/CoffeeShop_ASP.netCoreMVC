using CoffeeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Repository
{
    interface IRoleRepository
    {
        Task<bool> Add(Role role);
        Task<bool> Delete(int id);
        Task<bool> Update(Role roleChanges);
        Task<Role> FindByIdAsync(int roleId);
        Task<IEnumerable<Role>> GetAll();
    }
}
