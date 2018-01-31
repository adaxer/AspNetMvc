using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Southwind.Contracts.Interfaces;
using Southwind.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestService.Controllers
{
    [Route("api/[controller]")] 
    [Produces("application/json")]
    [Authorize]
    public class ShopController : Controller
    {
        private IShopService shopService;

        public ShopController(IShopService service)
        {
            shopService = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Categories()
        {
            var result = await Task.Run(()=>shopService.GetCategoriesAsync());
            // Entspricht folgendem:
            //result = await Task.Factory.StartNew<IEnumerable<Category>>(
            //    () => (IEnumerable<Category>)shopService.GetCategories(), 
            //    TaskCreationOptions.None);
            return Ok(result);
        }

        //[HttpGet("[action]")]
        //public IActionResult Categories()
        //{
        //    var result = shopService.GetCategories().Result;
        //    return Ok(result);
        //}
    }
}
