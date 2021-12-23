using ShopApp.Models;
using System.Collections.Generic;

namespace ShopApp.Dtos
{
    public class DisplayTags
    {
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public DisplayTags(List<Tag> tags)
        {
            Tags = tags;
        }
    }
}
