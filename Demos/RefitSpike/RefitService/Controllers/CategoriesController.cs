using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using RefitContracts;

namespace RefitService.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        // GET api/values
        [HttpGet("[action]")]
        public IActionResult All()
        {
            var fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(2));

            var categories = fixture.CreateMany<Category>(5);
            return Ok(categories);
        }
    }
}
