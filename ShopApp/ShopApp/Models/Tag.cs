using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters long.")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<ShopItemTag> ShopItemTags { get; set; }
    }
}
