using Microsoft.AspNetCore.Mvc;
using RefitContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefitService.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        [HttpPost("[action]")]
        public void Add([FromBody]Product value)
        {
            Console.WriteLine(value);
        }
    }
}
