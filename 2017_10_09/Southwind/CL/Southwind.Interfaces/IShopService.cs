using Southwind.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Southwind.Contracts.Interfaces
{
    public interface IShopService
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<Product> GetProducts(int categoryId);

        void CreateCategory(Category category);
    }
}
