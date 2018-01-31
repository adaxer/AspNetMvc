using Southwind.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Southwind.Interfaces
{
    public interface IShopService
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}
