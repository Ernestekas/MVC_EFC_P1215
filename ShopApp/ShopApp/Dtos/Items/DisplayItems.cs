using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Dtos.Items
{
    public class DisplayItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int ShopId { get; set; }
        public string ItemTags { get; set; }
    }
}
