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

        public IActionResult AddNewShop(Shop model = null)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitNewShop(Shop model)
        {
            var test = ModelState;
            try
            {
                _shopService.CreateNewShop(model);
                return RedirectToAction("AllShops");
            }
            catch
            {
                return RedirectToAction("AddNewShop", model);
            }
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

        public IActionResult DeleteShop(Shop model)
        {
            _itemsService.DeleteAllItemsFromShop(model);
            _shopService.DeleteShop(model);
            return RedirectToAction("AllShops");
        }

        public IActionResult UpdateShop(Shop model)
        {
            return View(model);
        }

        public IActionResult SubmitUpdateShop(Shop model)
        {
            try
            {
                _shopService.UpdateShop(model);
                return RedirectToAction("GoToShop", model);
            }
            catch
            {
                return RedirectToAction("UpdateShop", model);
            }
        }

    }
}
