using Southwind.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Southwind.Data;
using System.Net.Http;

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

        public IEnumerable<Category> GetCategories()
        {
            return restService.GetDataAsync<IEnumerable<Category>>($"{baseUrl}/shop/categories").Result;
        }

        public IEnumerable<Product> GetProducts(int categoryId)
        {
            return restService.GetDataAsync<IEnumerable<Product>>($"{baseUrl}/shop/productspercategory/{categoryId}").Result;
        }
    }
}
