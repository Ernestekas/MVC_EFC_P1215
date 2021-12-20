using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.GenRep
{
    public class GenericRepo<T> : IGenericRepo<T> where T: class
    {
        private DataContext _context;
        private DbSet<T> _table;

        public GenericRepo(DataContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
