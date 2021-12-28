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

        public List<ShopItem> GetAll() //Remove name from method
        {
            return _context.ShopItems.Include(x => x.Shop).ToList();
        }

        public void Create(ShopItem item)
        {
            _context.ShopItems.Add(item);
            _context.SaveChanges();
        }

        public void Update(ShopItem item)
        {
            _context.ShopItems.Update(item);
            _context.SaveChanges();
        }
        //Separate create or update
        public void CreateOrUpdate(ShopItem model, Shop shop, bool updating = false)
        {
            ShopItem item = model;

            if (updating)
            {
                item = _context.ShopItems.Include(i => i.Shop).FirstOrDefault(i => i.Id == model.Id);
                item.Name = model.Name;
                item.ExpiryDate = model.ExpiryDate;
            }

            if (shop != null)
            {
                item.Shop = shop;
            }

            if (updating && shop == null)
            {
                throw new Exception(); //This is not good exception to throw
            }

            if (updating)
            {
                _context.Update(item);
            }
            else
            {
                _context.ShopItems.Add(item);
            }

            _context.SaveChanges();
        }

        public void Delete(int itemId)
        {
            _context.ShopItems.Remove(GetById(itemId));
            _context.SaveChanges();
        }

        //Method names should beconsistent
        public ShopItem GetFromDb(ShopItem model)
        {
            return _context.ShopItems.Include(s => s.Shop).FirstOrDefault(m => m.Id == model.Id);
        }

        public ShopItem GetById(int itemId)
        {
            return _context.ShopItems.Include(s => s.Shop).FirstOrDefault(m => m.Id == itemId);
        }
        public List<ShopItem> GetAllByShopId(int shopId)
        {
            return _context.ShopItems.Include(i => i.Shop).Where(i => i.Shop.Id == shopId).ToList();
        }

        public ShopItem GetByName(string name)
        {
            return GetAll().FirstOrDefault(i => i.Name == name);
        }
    }
}
