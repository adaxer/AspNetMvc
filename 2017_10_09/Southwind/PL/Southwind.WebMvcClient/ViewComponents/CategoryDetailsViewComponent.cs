using Microsoft.AspNetCore.Mvc;
using Southwind.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Southwind.Presentation.Web.ViewComponents
{
    public class CategoryDetailsViewComponent : ViewComponent
    {
        private readonly IShopService shopService;

        public CategoryDetailsViewComponent(IShopService shop)
        {
            shopService=shop;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var items = shopService.GetProducts(categoryId).ToList();
            await Task.CompletedTask;
            return View(items);
        }
    }
}
