using Southwind.Interfaces;
using Southwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.BusinessObjects
{
    public class ShopService : IShopService
    {
        IRepository<Category> _repository;

        public ShopService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            var result = _repository.Find().AsEnumerable();
            return Task.FromResult(result);
        }
    }
}
