using Southwind.Interfaces;
using Southwind.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.CommonLib
{
    public class RestShopService : IShopService
    {
        private IRestService _restService;
        private string _shopUrl;

        public RestShopService(IRestService restService, string shopUrl)
        {
            _restService = restService;
            _shopUrl = shopUrl;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _restService.GetDataAsync<IEnumerable<Category>>($"{_shopUrl}/categories/all");
        }
    }
}
