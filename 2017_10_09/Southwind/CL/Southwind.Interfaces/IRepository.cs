using System;
using System.Linq;
using System.Linq.Expressions;

namespace Southwind.Contracts.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Delete(T entity);

        IQueryable<T> Find(Expression<Func<T, bool>> filter=null);

        //void SetUOW(IUoW uow);
    }

}
