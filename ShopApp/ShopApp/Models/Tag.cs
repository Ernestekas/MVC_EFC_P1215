using System.Collections.Generic;

namespace ShopApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<ShopItemTag> ShopItemTags { get; set; }
    }
}
