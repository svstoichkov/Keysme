namespace Keysme.Data
{
    using System;
    using System.Linq;

    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        //IQueryable<T> AllWithDeleted();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        //void HardDelete(T entity);

        //void HardDelete(object id);

        T Attach(T entity);

        void Detach(T entity);

        int SaveChanges();
    }
}
