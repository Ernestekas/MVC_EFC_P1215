using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Shop
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        public List<ShopItem> ShopItems { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
