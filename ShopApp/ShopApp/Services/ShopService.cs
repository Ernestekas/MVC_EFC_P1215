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

        // Optimization in proccess.
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

        public void CreateNewShop(Shop model)
        {
            if(string.IsNullOrWhiteSpace(model.Name))
            {
                throw new Exception();
            }
            _context.Shops.Add(model);
            _context.SaveChanges();
        }

        public void DeleteShop(Shop model)
        {
            Shop selected = GetShop(model);
            _context.Remove(selected);
            _context.SaveChanges();
        }

        public void UpdateShop(Shop model)
        {
            if(string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < 3)
            {
                throw new Exception();
            }

            Shop selected = GetShopFromId(model.Id.ToString());
            selected.Name = model.Name;
            _context.Update(selected);
            _context.SaveChanges();
        }

        public void CheckIfShopExists(string shopId)
        {
            Shop shop = new Shop();
            if(int.TryParse(shopId, out int id))
            {
                shop = _context.Shops.FirstOrDefault(s => s.Id == id);
            }
            else
            {
                throw new Exception();
            }

            if(shop == null)
            {
                throw new Exception();
            }
        }
    }
}
