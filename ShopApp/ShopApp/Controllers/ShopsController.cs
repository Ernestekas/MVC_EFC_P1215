using Microsoft.AspNetCore.Mvc;
using ShopApp.Data;
using ShopApp.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ShopApp.Services;

namespace ShopApp.Controllers
{
    public class ShopsController : Controller
    {
        private readonly ShopService _shopService;
        private readonly ItemsService _itemsService;
        public ShopsController(ShopService shopService, ItemsService itemsService)
        {
            _shopService = shopService;
            _itemsService = itemsService;
        }

        public IActionResult AllShops()
        {
            return View(_shopService.GetAllShops());
        }

        public IActionResult Add()
        {
            return View(new Shop());
        }

        [HttpPost]
        public IActionResult Add(Shop model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            _shopService.CreateOrUpdate(model);
            return RedirectToAction(nameof(AllShops));
        }

        public IActionResult ToShop(Shop model)
        {
            model.ShopItems = _itemsService.GetAllByShop(model);
            return View(model);
        }

        public IActionResult Update(int shopId)
        {
            Shop shop = _shopService.GetById(shopId);
            return View(shop);
        }

        [HttpPost]
        public IActionResult Update(Shop model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            _shopService.CreateOrUpdate(model, true);
            return RedirectToAction(nameof(ToShop), model);
        }

        public IActionResult Delete(Shop model)
        {
            _shopService.Delete(model);
            return RedirectToAction(nameof(AllShops));
        }
    }
}
