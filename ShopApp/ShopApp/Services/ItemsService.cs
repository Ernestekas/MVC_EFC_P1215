using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Services
{
    public class ItemsService
    {
        private DataContext _context;
        public ItemsService(DataContext context)
        {
            _context = context;
        }

        public List<ShopItem> GetAllItems()
        {
            return _context.ShopItems.Include(x => x.Shop).ToList();
        }
        public void SubmitNewItemToDb(ShopItem model, string shopId)
        {
            if(string.IsNullOrWhiteSpace(model.Name))
            {
                throw new Exception();
            }

            Shop shop = _context.Shops.FirstOrDefault(s => s.Id == int.Parse(shopId));

            if(shop != null)
            {
                model.Shop = shop;
            }
            
            _context.ShopItems.Add(model);
            _context.SaveChanges();
        }

        public void SubmitUpdateItemToDb(ShopItem model, string shopId)
        {
            ShopItem selected = _context.ShopItems.FirstOrDefault(m => m.Id == model.Id);

            if(string.IsNullOrWhiteSpace(model.Name))
            {
                throw new Exception();
            }
            
            Shop shop = _context.Shops.FirstOrDefault(s => s.Id == int.Parse(shopId));
            
            if(shop != null)
            {
                selected.Shop = shop;
            }
            _context.SaveChanges();
        }
    }
}
