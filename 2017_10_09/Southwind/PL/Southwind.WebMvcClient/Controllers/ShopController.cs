using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Southwind.Contracts.Interfaces;
using Southwind.Data;

namespace Southwind.WebMvcClient.Controllers
{
    public class ShopController : Controller
    {
        IShopService shop;

        public ShopController(IShopService shopService)
        {
            shop = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var cats = await Task.Run<IEnumerable<Category>>(() => shop.GetCategories().ToList());
            return View(cats);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDetails(int id)
        {
            var cat = await Task.Run<Category>(() => shop.GetCategories().SingleOrDefault(c => c.CategoryId == id));
            if (cat == null)
                return NotFound();
            else
                return View(cat);
        }

        [HttpGet]
        public async Task<IActionResult> LoadComponent(int id)
        {
            return await Task.Run<IActionResult>(() => ViewComponent("CategoryDetails", new { categoryId = id }));
        }

        // GET: Shop/Create
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: Shop/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind] Category category)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await Task.Run(() => shop.CreateCategory(category));

                    return RedirectToAction(nameof(Categories));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shop/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Categories));
            }
            catch
            {
                return View();
            }
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