using Newtonsoft.Json;
using RefitContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RefitClient
{
    public class ShopService : IShopService
    {
        string _baseUrl = "http://localhost:49424/api/";

        public async Task<IEnumerable<Category>> GetCategories()
        {
            using (var client=new HttpClient())
            {
                string json = await client.GetStringAsync($"{_baseUrl}categories/all");
                return JsonConvert.DeserializeObject<List<Category>>(json);
            }
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public async Task SaveProduct(Product product)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{_baseUrl}products/add", content);
            }
        }
    }
}
