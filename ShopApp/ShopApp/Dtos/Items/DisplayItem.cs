using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Dtos.Items
{
    public class DisplayItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string ItemTags { get; set; }
        public List<Tag> Tags { get; set; }

        public DisplayItem() { }

        public DisplayItem(ShopItem item, List<Tag> itemTags)
        {
            Id = item.Id;
            Name = item.Name;
            ExpiryDate = item.ExpiryDate;
            ItemTags = string.Join(", ", itemTags.Select(t => t.Name));
            Tags = itemTags;
        }

        public DisplayItem(UpdateItem item)
        {
            Id = item.Id;
            Name = item.Name;
            ExpiryDate = item.ExpiryDate;
            
        }
    }
}
