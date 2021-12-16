using ShopApp.Data;
using ShopApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Services
{
    public class ShopService
    {
        private DataContext _context;
        public ShopService(DataContext context)
        {
            _context = context;
        }

        public List<Shop> GetAllShops()
        {
            return _context.Shops.ToList();
        }
    }
}
