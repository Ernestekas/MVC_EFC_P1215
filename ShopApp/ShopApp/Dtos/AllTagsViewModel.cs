using ShopApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Dtos
{
    public class AllTagsViewModel
    {
        public List<Tag> Tags { get; set;}
        public List<string> TagsNames { get; set;} = new List<string>();

        public AllTagsViewModel(List<Tag> tags)
        {
            if(tags != null)
            {
                TagsNames.AddRange(tags.Select(x => x.Name));
            }
        }

    }

    
}
