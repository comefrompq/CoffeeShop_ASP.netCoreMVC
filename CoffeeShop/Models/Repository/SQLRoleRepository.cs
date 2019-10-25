using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Data;
using CoffeeShop.Data.Models;

namespace CoffeeShop.Models.Repository
{
    public class SQLRoleRepository : IRoleRepository
    {
        private readonly CoffeeShopDbContext _context;

        public SQLRoleRepository(CoffeeShopDbContext context)
        {
            this._context = context;
        }
        public Role Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public Role Delete(int Id)
        {
            Role role = _context.Roles.Find(Id);
            if(role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
            return role;
        }

        public IEnumerable<Role> GetAllRole()
        {
            return _context.Roles;
        }

        public Role GetRole(int Id)
        {
            return _context.Roles.Find(Id);
        }

        public Role Update(Role roleChanges)
        {
            var role = _context.Roles.Attach(roleChanges);
            role.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return roleChanges;
        }
    }
}
