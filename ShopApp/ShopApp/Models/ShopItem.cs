using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ShopApp.Models
{
    public class ShopItem
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Name")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        public string Name { get; set; }

        [DisplayName("Expiry Date")]
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;

        [DisplayName("Shop")]
        public Shop? Shop { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
