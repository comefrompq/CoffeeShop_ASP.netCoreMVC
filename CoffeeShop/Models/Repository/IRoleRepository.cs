using CoffeeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.Repository
{
    interface IRoleRepository
    {
        Role GetRole(int Id);
        IEnumerable<Role> GetAllRole();
        Role Add(Role role);
        Role Update(Role roleChanges);
        Role Delete(int Id);
    }
}
