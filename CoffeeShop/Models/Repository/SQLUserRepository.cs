using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Data;
using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Models.Repository
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly CoffeeShopDbContext _context;
        public SQLUserRepository(CoffeeShopDbContext dbContext)
        {
            _context = dbContext;
        }
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }
        public async Task<bool> Add(User user)
        {
            var role = _context.Roles.Where(r => r.Name == "user").FirstOrDefault();
            user.Role = role;

            var res = _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();

            if (res == null && user.Password != null)
            {
                user.Password = MD5Hash(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == userId);
        }
        public async Task<User> CanSignIn(User user)
        {
            var res = _context.Users.Include(x => x.Role).Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (res != null && MD5Hash(user.Password) == res.Password) return res;
            return null;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Include(x => x.Role).ToListAsync();
        }

        public async Task<bool> Update(User user)
        {
            user.Password = MD5Hash(user.Password);
            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
