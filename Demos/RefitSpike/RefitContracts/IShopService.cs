using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RefitContracts
{
    public interface IShopService
    {
        [Get("/categories/all")]
        Task<IEnumerable<Category>> GetCategories();

        [Get("/products/all")]
        Task<IEnumerable<Product>> GetProducts();

        [Post("/products/add")]
        Task SaveProduct([Body]Product product);
    }
}
