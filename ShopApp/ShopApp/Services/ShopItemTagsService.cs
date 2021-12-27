using ShopApp.Data;
using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Services
{
    public class ShopItemTagsService
    {
        private DataContext _context;
        public ShopItemTagsService(DataContext context)
        {
            _context = context;
        }

        public void Create(int itemId, List<int> tagsId)
        {
            List<ShopItemTag> itemTags = new List<ShopItemTag>();

            foreach(var id in tagsId)
            {
                ShopItemTag itemTag = new ShopItemTag()
                {
                    ShopItemId = itemId,
                    TagId = id
                };

                itemTags.Add(itemTag);
            }

            _context.ShopItemTags.AddRange(itemTags);
            _context.SaveChanges();
        }
    }
}
