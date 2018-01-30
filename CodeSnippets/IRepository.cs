using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Southwind.Models;
using System.Linq.Expressions;

namespace Southwind.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Delete(T entity);

        IQueryable<T> Find(Expression<Func<T, bool>> filter=null);

        //void SetUOW(IUoW uow);
    }

}
