using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Southwind.Contracts.Models;
using Southwind.Contracts.Interfaces;

namespace WebApp.Controllers
{
    public class ShopController : Controller
    {
        private IShopService shopService;

        public ShopController(IShopService shopService)
        {
            this.shopService = shopService;
        }

        // GET: Shop
        public async Task<IActionResult> Categories()
        {
            var categories = await shopService.GetCategoriesAsync();
            return View(categories);
        }

        // GET: Shop/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Categories));
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = (await shopService.GetCategoriesAsync()).SingleOrDefault(c=>c.CategoryId == id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: Shop/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind] Category category)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    await shopService.SaveCategoryAsync(category);
                    return RedirectToAction(nameof(Categories));
                }
            }
            catch
            {
                return View();
            }
            return BadRequest();
        }

        // GET: Shop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shop/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Categories));
            }
            catch
            {
                return View();
            }
        }
    }
}