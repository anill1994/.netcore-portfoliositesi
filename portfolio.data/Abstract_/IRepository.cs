using System.Collections.Generic;

namespace portfolio.data.Abstract
{
    public interface IRepository<T>
    {
        T GetById(int id);
        T GetOne();
        List<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}