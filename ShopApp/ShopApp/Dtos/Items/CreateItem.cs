﻿using ShopApp.Models;
using System;
using System.Collections.Generic;

namespace ShopApp.Dtos
{
    public class CreateItem
    {
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.Now;
        public int ShopId { get; set; }
        public List<Tag> Tags { get; set; }
        public List<int> SelectedTagsIds { get; set; }

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
