using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ShopApp.Models
{
    public class ShopItem
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name field is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Name must be at least 5 characters long.")]
        public string Name { get; set; }

        [DisplayName("Expiry Date")]
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;

        [DisplayName("Shop")]
        public Shop Shop { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<ShopItemTag> ShopItemTags { get; set; }
    }
}
