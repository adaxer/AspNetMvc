using Southwind.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Southwind.Contracts.Model;

namespace Southwind.BL
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> m_Repository;

        public CategoryService(IRepository<Category> repository)
        {
            m_Repository = repository;
        }

        public IEnumerable<Category> GetCategories()
        {
            var result = m_Repository.Find().ToList();
            return result;
        }
    }
}
