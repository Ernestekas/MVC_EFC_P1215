using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using ShopApp.Services;
using System.Collections.Generic;

namespace ShopApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemsService _itemsService;
        private readonly ShopService _shopService;
        public ItemsController(ItemsService itemsService, ShopService shopService)
        {
            _itemsService = itemsService;
            _shopService = shopService;
        }

        public IActionResult AllItems()
        {
            List<ShopItem> model = _itemsService.GetAllItems();

            return View(model);
        }

        public IActionResult Add()
        {
            return View(new ShopItem());
        }

        [HttpPost]
        public IActionResult Add(ShopItem model, int shopId)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            Shop shop = _shopService.GetById(shopId);
            _itemsService.CreateOrUpdate(model, shop);

            return RedirectToAction(nameof(AllItems));
        }

        public IActionResult AddFromShop(Shop model)
        {
            ModelState.Clear();
            ShopItem emptyItem = new ShopItem() { Name="", Shop = model };
            return View(emptyItem);
        }

        [HttpPost]
        public IActionResult AddFromShop(ShopItem model, int shopId)
        {
            model.Id = 0;
            Shop shop = _shopService.GetById(shopId);
            if (!ModelState.IsValid)
            {
                model.Shop = shop;
                return View(model);
            }

            try
            {
                _itemsService.CreateOrUpdate(model, shop);
                return RedirectToAction("ToShop", "Shops", shop);
            }
            catch
            {
                shop = new Shop() { Id = shopId };
                ModelState.AddModelError("shopId", "There is no such shop.");
                return View(model);
            }
        }

        public IActionResult Update(ShopItem model)
        {
            ShopItem item = _itemsService.GetFromDb(model);
            return View(item);
        }

        [HttpPost]
        public IActionResult Update(ShopItem model, int shopId)
        {
            if(!ModelState.IsValid)
            {
                model.Shop = new Shop() { Id = shopId };
                return View(model);
            }

            try
            {
                Shop shop = _shopService.GetById(shopId);
                _itemsService.CreateOrUpdate(model, shop, true);

                return RedirectToAction(nameof(AllItems));
            }
            catch
            {
                ModelState.AddModelError("shopId", "There is no such shop.");
                return View(model);
            }
        }

        public IActionResult UpdateFromShop(ShopItem model)
        {
            ShopItem item = _itemsService.GetFromDb(model);
            ViewBag.RedirectToShop = item.Shop.Id;

            return View(item);
        }

        [HttpPost]
        public IActionResult UpdateFromShop(ShopItem model, int shopId, int redirectToShop)
        {
            if(!ModelState.IsValid)
            {
                model.Shop = new Shop() { Id = shopId };
                return View(model);
            }

            try
            {
                Shop shop = _shopService.GetById(shopId);
                Shop redirect = _shopService.GetById(redirectToShop);

                _itemsService.CreateOrUpdate(model, shop, true);
                return RedirectToAction("ToShop", "Shops", redirect);
            }
            catch
            {
                ModelState.AddModelError("shopId", "There is no such shop.");
                ViewBag.RedirectToShop = redirectToShop;
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Delete(ShopItem model)
        {
            _itemsService.Delete(model);

            return RedirectToAction(nameof(AllItems));
        }
    }
}
