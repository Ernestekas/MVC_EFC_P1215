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

        public IActionResult AddNewShop()
        {
            return View();
        }

        public IActionResult GoToShop(Shop model)
        {
            return View(_shopService.GetShop(model));
        }

        public IActionResult UpdateItem(ShopItem model)
        {
            return View(_itemsService.GetItem(model));
        }

        public IActionResult SubmitUpdate(ShopItem model, string shopId, string oldShopId)
        {
            try
            {
                _itemsService.SubmitDataAndUpdateDb(model, shopId, true);
                Shop redirect = _shopService.GetShopFromId(oldShopId);
                return RedirectToAction("GoToShop", redirect);
            }
            catch
            {
                return RedirectToAction("UpdateItem", model);
            }
            
        }
    }
}
