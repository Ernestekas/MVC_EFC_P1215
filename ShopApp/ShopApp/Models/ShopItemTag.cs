namespace ShopApp.Models
{
    public class ShopItemTag
    {
        public int ShopItemId { get; set; }
        public ShopItem ShopItem {get; set;}
        public int TagId { get; set; }
        public Tag Tag {get; set;}
    }
}
