using CoffeeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.AccountViewModels
{
    public class ManagementViewModel
    {
        public IEnumerable<User> users { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Product> products { get; set; }
    }
}
