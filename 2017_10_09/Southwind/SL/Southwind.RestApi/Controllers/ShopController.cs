using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Southwind.Contracts.Interfaces;
using Southwind.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Southwind.RestApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ShopController : Controller
    {
        IShopService shop;

        public ShopController(IShopService shopService)
        {
            shop = shopService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Categories()
        {
            var cats = await Task.Run<IEnumerable<Category>>(() => shop.GetCategories().ToList());
            return Ok(cats);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ProductsPerCategory(int id)
        {
            var prods = await Task.Run<IEnumerable<Product>>(() => shop.GetProducts(id).ToList());
            return Ok(prods);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Categories(int id)
        {
            var cat = await Task.Run<Category>(() => shop.GetCategories().SingleOrDefault(c => c.CategoryId == id));
            if (cat == null)
                return NotFound();
            else
                return Ok(cat);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            await Task.Run(() => shop.CreateCategory(category));
            return CreatedAtAction(nameof(Categories), new { id = category.CategoryId }, category);
        }
    }
}
