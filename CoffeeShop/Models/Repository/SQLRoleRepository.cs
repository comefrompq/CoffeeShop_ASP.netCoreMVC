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
        public SQLRoleRepository(CoffeeShopDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<bool> Add(Role role)
        {
            _context.Add(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Role> FindByIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return _context.Roles;
        }

        public async Task<bool> Update(Role roleChanges)
        {
            _context.Roles.Update(roleChanges);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
