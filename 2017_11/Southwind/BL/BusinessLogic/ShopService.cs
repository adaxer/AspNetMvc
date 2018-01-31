using Southwind.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Southwind.Contracts.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Southwind.Logic.BusinessLogic
{
    public class ShopService : IShopService
    {
        private IRepository<Category> repository;

        public ShopService(IRepository<Category> catRepository)
        {
            repository = catRepository;
        }

        public Task DeleteCategoryAsync(Category cat)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = repository.Find();
            return Task.FromResult<IEnumerable<Category>>(categories.ToList());
        }

        public Task SaveCategoryAsync(Category cat)
        {
            return Task.Factory.StartNew(()=>repository.Add(cat));
        }
    }
}
