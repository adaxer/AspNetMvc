using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Southwind.Interfaces;
using Southwind.Models;

namespace Southwind.RestApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ShopController : Controller
    {
        private IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        // GET api/values
        [HttpGet]
        [Route("categories/all")]
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var result = (await _shopService.GetCategories()).ToList();

            return result;
        }

        // GET api/values/5
        [HttpGet("categories/{id}")]
        public string Get(int id)
        {
            return "value"+id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
