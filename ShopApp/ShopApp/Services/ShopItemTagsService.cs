using ShopApp.Data;
using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ShopApp.Services
{
    public class ShopItemTagsService
    {
        private DataContext _context;
        private SqlConnection _connection;
        public ShopItemTagsService(DataContext context, SqlConnection connection)
        {
            _context = context;
            _connection = connection;
        }

        public void Create(int itemId, List<int> tagsId)
        {
            List<ShopItemTag> itemTags = new List<ShopItemTag>();

            foreach(var id in tagsId)
            {
                ShopItemTag itemTag = new ShopItemTag()
                {
                    ShopItemId = itemId,
                    TagId = id
                };

                itemTags.Add(itemTag);
            }

            _context.ShopItemTags.AddRange(itemTags);
            _context.SaveChanges();
        }

        public List<ShopItemTag> GetAll()
        {
            return _context.ShopItemTags.ToList();
        }

        public List<int> GetTagsByItemId(int itemId)
        {
            return GetAll().Where(i => i.ShopItemId == itemId).Select(x => x.TagId).ToList();
        }

        public void DeleteByTagId(int tagId)
        {
            string query = $"DELETE FROM dbo.ShopItemTags WHERE TagId={tagId}";
            _connection.Query<ShopItemTagsService>(query);
        }

        public void DeleteByItemId(int itemId)
        {
            string query = $"DELETE FROM dbo.ShopItemTags WHERE ShopItemId={itemId}";
            _connection.Query<ShopItemTagsService>(query);
        }
    }
}
