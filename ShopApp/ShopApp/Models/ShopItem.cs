using System;

namespace ShopApp.Models
{
    public class ShopItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;
        public Shop Shop { get; set; }
    }
}
