using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Southwind.Interfaces;
using Southwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Southwind.ApiHost
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    //[RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        public CategoriesController(ICategoryService service)
        {
            logic = service;
        }

        //public class Category
        //{
        //    [JsonProperty("id")]
        //    public int Id { get; set; }
        //    //[JsonProperty("categoryName")]
        //    public string CategoryName { get; set; }
        //    [JsonProperty("description")]
        //    public string Description { get; set; }
        //}

            ICategoryService logic;

        // GET api/values 
        [HttpGet, Route("allcategories")]
        public IHttpActionResult Irgendwas()
        {
            return Get();
        }

        public IHttpActionResult Get()
        {
            var result = logic.GetCategories();
            foreach (var item in result)
            {
                item.Picture = null;
            }
            return Json<IEnumerable<Category>>(result, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    } 
}
