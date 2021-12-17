using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Models;
using System;
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

        public Shop GetShop(Shop model)
        {
            return _context.Shops.Include(s => s.ShopItems).FirstOrDefault(s => s.Id == model.Id);
        }

        public Shop GetShopFromId(string shopId)
        {
            return _context.Shops.Include(s => s.ShopItems).FirstOrDefault(s => s.Id == int.Parse(shopId));
        }
    }
}
