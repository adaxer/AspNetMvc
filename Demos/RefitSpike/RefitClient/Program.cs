using AutoFixture;
using Refit;
using RefitContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefitClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CallService();

            Console.ReadLine();
        }

        private static async void CallService()
        {
            var svc = new ShopService();
            var cats = await svc.GetCategories();

            var fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            var product = fixture.Create<Product>();
            await svc.SaveProduct(product);


            // Now Refit :-)
            var shop = RestService.For<IShopService>("http://localhost:49424/api");

            cats = await shop.GetCategories();

            await shop.SaveProduct(product);

        }
    }
}
