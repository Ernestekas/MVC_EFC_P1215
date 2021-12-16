using System;
using System.ComponentModel;

namespace ShopApp.Models
{
    public class ShopItem
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Expiry Date")]
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;
        [DisplayName("Shop")]
        public Shop? Shop { get; set; }
    }
}
