using ShopApp.Dtos.Items;
using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Dtos.Items
{
    public class UpdateItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OldName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime OldExpiryDate { get; set; }
        public int ShopId { get; set; }
        public int OldShopId { get; set; }
        public List<Tag> Tags { get; set; }
        public List<int> SelectedTagsIds { get; set; }
        public List<int> OldSelectedTagsIds { get; set; }

        public UpdateItem() { }

        public UpdateItem(DisplayItem itemFromList)
        {
            List<int> tagsIds = itemFromList.Tags.Select(t => t.Id).ToList();

            Id = itemFromList.Id;
            Name = itemFromList.Name;
            ExpiryDate = itemFromList.ExpiryDate;
            ShopId = itemFromList.ShopId;
            OldSelectedTagsIds = tagsIds;
        }
        public ShopItem GetItem(Shop shop)
        {
            ShopItem result = new ShopItem()
            {
                Name = this.Name,
                ExpiryDate = this.ExpiryDate,
                Shop = shop
            };

            return result;
        }
    }
}
