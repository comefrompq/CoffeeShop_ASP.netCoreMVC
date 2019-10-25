using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Data;
using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Models.Repository
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly CoffeeShopDbContext _context;

        public SQLUserRepository(CoffeeShopDbContext context)
        {
            this._context = context;
        }
        public async Task<User> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> CanSignin(string user, string password)
        {
            User res = await _context.Users.Include(r => r.Role).Where(x => x.UserName == user).FirstOrDefaultAsync();
            
            if(res != null)
            {
                if(res.Password == password)
                    return res;
            }
            return null;
        }

        public async Task<User> Delete(int Id)
        {
            User user = _context.Users.Find(Id);
            if(user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return _context.Users;
        }

        public async Task<User> GetUser(int Id)
        {
            return _context.Users.Find(Id);           
        }

        public async Task<User> Update(User userChanges)
        {
            var user = _context.Users.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return userChanges;
        }
    }
}
