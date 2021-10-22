using ShareHQ.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShareHQ.Data
{ 
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);            
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
            _context.SaveChanges();
        }

        public void Remove(T item)
        {
            _context.Set<T>().Remove(item);
            _context.SaveChanges();            
        }
    }
}
