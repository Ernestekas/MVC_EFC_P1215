using ShopApp.Data;
using ShopApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Services
{
    public class TagsService
    {
        private DataContext _context;
        public TagsService(DataContext context)
        {
            _context = context;
        }

        public List<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }
    }
}
