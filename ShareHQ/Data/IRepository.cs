using System.Collections.Generic;

namespace ShareHQ.Data
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();

        T GetById(int id);        

        void Add(T obj);

        void Update(T obj);
        
        void Remove(T obj);
    }
}
