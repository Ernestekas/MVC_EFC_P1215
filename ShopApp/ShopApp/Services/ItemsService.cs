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

        public void SubmitDataAndUpdateDb(ShopItem model, string shopId, bool updatingCurrentData = false)
        {
            Shop shop = new Shop();
            ShopItem selected = model;

            if (updatingCurrentData)
            {
                shop = _context.Shops.FirstOrDefault(x => x.Id == int.Parse(shopId));

                selected = _context.ShopItems.Include(i => i.Shop).FirstOrDefault(m => m.Id == model.Id);
                selected.Shop = shop;
                selected.Name = model.Name;
            }
            else if(!string.IsNullOrWhiteSpace(shopId))
            {
                shop = _context.Shops.FirstOrDefault(x => x.Id == int.Parse(shopId));
                selected.Shop = shop;
            }

            if (!updatingCurrentData)
            {
                _context.ShopItems.Add(selected);
            }
            else
            {
                _context.Update(selected);
            }
            _context.SaveChanges();
        }

        public ShopItem GetItem(ShopItem model)
        {
            return _context.ShopItems.Include(s => s.Shop).FirstOrDefault(m => m.Id == model.Id);
        }

        public void DeleteItem(ShopItem model)
        {
            _context.ShopItems.Remove(model);
            _context.SaveChanges();
        }

        public void DeleteAllItemsFromShop(Shop model)
        {
            Shop selected = _context.Shops.Include(s => s.ShopItems).FirstOrDefault(s => s.Id == model.Id);
            List<ShopItem> items = selected.ShopItems;
            _context.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}
