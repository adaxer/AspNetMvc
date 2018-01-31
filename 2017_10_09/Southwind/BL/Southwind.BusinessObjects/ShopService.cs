using Southwind.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Southwind.Data;

namespace Southwind.BusinessObjects
{
    public class ShopService : IShopService
    {
        IRepository<Category> categories;
        IRepository<Product> products;

        public ShopService(IRepository<Category> catRepo, IRepository<Product> prodRepo)
        {
            categories = catRepo;
            products = prodRepo;
        }

        public void CreateCategory(Category category)
        {
            try
            {
                categories.Add(category);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            return categories.Find();
        }

        public IEnumerable<Product> GetProducts(int categoryId)
        {
            return products.Find((Product p) => p.CategoryId == categoryId);
        }
    }
}
