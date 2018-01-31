﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Southwind.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Delete(T entity);

        IQueryable<T> Find(Expression<Func<T, bool>> filter = null);
    }
}