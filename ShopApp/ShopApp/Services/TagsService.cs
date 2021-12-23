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

        public void Create(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public void Update(Tag tag)
        {
            _context.Tags.Update(tag);
            _context.SaveChanges();
        }

        public void Delete(Tag tag)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();
        }

        public Tag GetById(int tagId)
        {
            return _context.Tags.SingleOrDefault(t => t.Id == tagId);
        }
    }
}
