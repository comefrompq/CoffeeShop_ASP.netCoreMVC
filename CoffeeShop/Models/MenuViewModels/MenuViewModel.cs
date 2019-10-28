using CoffeeShop.Data;
using CoffeeShop.Data.Models;
using CoffeeShop.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.MenuViewModels
{
    public class MenuViewModel 
    {
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Product> products { get; set; }
    }
}
