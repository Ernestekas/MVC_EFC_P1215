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
            ShopItem selected = model;
            Shop shop = _context.Shops.FirstOrDefault(s => s.Id == int.Parse(shopId));

            if (updatingCurrentData)
            {
                selected = _context.ShopItems.FirstOrDefault(m => m.Id == selected.Id);
                selected.Name = model.Name;
            }

            if(string.IsNullOrWhiteSpace(selected.Name) || (updatingCurrentData && shop == null))
            {
                throw new Exception();
            }
            
            if(shop != null)
            {
                selected.Shop = shop;
            }
            
            if(!updatingCurrentData)
            {
                _context.ShopItems.Add(selected);
            }
            else
            {
                _context.Attach(selected);
                _context.Entry(selected).State = EntityState.Modified;
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
    }
}
