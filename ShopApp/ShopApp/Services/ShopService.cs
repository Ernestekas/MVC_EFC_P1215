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

        public void CreateOrUpdate(Shop shop, bool update = false)
        {
            if(update)
            {
                Shop selected = GetById(shop.Id);
                selected.Name = shop.Name;
                _context.Update(selected);
                return;
            }

            _context.Shops.Add(shop);
            _context.SaveChanges();
        }

        public void Delete(Shop shop)
        {
            shop = GetShop(shop);

            _context.ShopItems.RemoveRange(shop.ShopItems);
            _context.SaveChanges();
            _context.Remove(shop);
            _context.SaveChanges();
        }

        public Shop GetById(int shopId)
        {
            return _context.Shops.Include(s => s.ShopItems).FirstOrDefault(s => s.Id == shopId);
        }

        public Shop GetShop(Shop model)
        {
            return _context.Shops.Include(s => s.ShopItems).FirstOrDefault(s => s.Id == model.Id);
        }

        public Shop GetShopFromId(string shopId)
        {
            Shop shop = new Shop();
            if(int.TryParse(shopId, out int id))
            {
                shop = _context.Shops.Include(s => s.ShopItems).FirstOrDefault(s => s.Id == id);
            }
            return shop;
        }
    }
}
