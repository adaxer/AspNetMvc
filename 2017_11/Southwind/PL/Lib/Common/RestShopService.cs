using Southwind.Contracts.Interfaces;
using Southwind.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Southwind.Presentation.Common
{
    public class RestShopService : IShopService
    {
        private string baseUrl;
        private IRestService restService;

        public RestShopService(IRestService rest, string baseUrl)
        {
            this.baseUrl = baseUrl;
            this.restService = rest;
        }

        public void CreateCategory(Category category)
        {
            restService.PostDataAsync<Category>(category, $"{baseUrl}/shop/createcategory").Wait();
        }

        public Task DeleteCategoryAsync(Category cat)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await restService.GetDataAsync<IEnumerable<Category>>($"{baseUrl}/shop/categories");
        }

        public IEnumerable<Product> GetProducts(int categoryId)
        {
            return restService.GetDataAsync<IEnumerable<Product>>($"{baseUrl}/shop/productspercategory/{categoryId}").Result;
        }

        public async Task SaveCategoryAsync(Category cat)
        {
            await restService.PostDataAsync<Category>(cat, $"{baseUrl}/shop/createcategory");
        }
    }
}
