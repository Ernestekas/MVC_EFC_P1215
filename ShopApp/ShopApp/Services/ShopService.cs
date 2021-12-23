using Microsoft.EntityFrameworkCore;
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

        public List<Shop> GetAll()
        {
            return _context.Shops.ToList();
        }

        /// <summary>
        /// I Would separate Create and update
        /// </summary>
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
            shop = GetById(shop.Id);

            _context.ShopItems.RemoveRange(shop.ShopItems);
            _context.SaveChanges(); //Maybe this savechanges is not needed
            _context.Remove(shop);
            _context.SaveChanges();
        }

        public Shop GetById(int shopId)
        {
            return _context.Shops.Include(s => s.ShopItems).FirstOrDefault(s => s.Id == shopId);
        }
    }
}
