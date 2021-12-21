using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using ShopApp.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        // OPTIMIZATION
        public IActionResult Add()
        {
            return View(new ShopItem());
        }

        // OPTIMIZATION
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

        // OPTIMIZATION
        public IActionResult Update(ShopItem model)
        {
            ShopItem item = _itemsService.GetFromDb(model);
            return View(item);
        }

        // OPTIMIZATION
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

        // OPTIMIZATION
        [HttpPost]
        public IActionResult Delete(ShopItem model)
        {
            _itemsService.Delete(model);

            return RedirectToAction(nameof(AllItems));
        }

        public IActionResult AddNewItem_Obsolete(Shop shopModel)
        {
            ShopItem emptyModel = new ShopItem();
            if (shopModel.Name != null)
            {
                ModelState.Clear();
                emptyModel.Shop = _shopService.GetShop(shopModel);
            }
            return View(emptyModel);
        }

        [HttpPost]
        public IActionResult SubmitNewItem_Obsolete(ShopItem model, string shopId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(shopId))
                {
                    _shopService.CheckIfShopExists(shopId);
                }

                _itemsService.SubmitDataAndUpdateDb(model, shopId);

                if (!string.IsNullOrWhiteSpace(shopId))
                {
                    return RedirectToAction("GoToShop", "Shops", _shopService.GetShopFromId(shopId));
                }
                return RedirectToAction("AllItems");
            }
            catch
            {
                return RedirectToAction("AddNewItem", model);
            }
        }
        // Validacijos testavimas. Pradžia.
        public IActionResult AddNewItem_Test(ShopItem model)
        {
            return View(model);
        }
        
        public IActionResult SubmitNewItem_Test(ShopItem model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("AddNewItem_Test", model);
            }

            return RedirectToAction("AllItems");
        }
        // Validacijos testavimas. Pabaiga.

        
        public IActionResult UpdateItem_Obsolete(ShopItem model)
        {
            return View(_itemsService.GetItem(model));
        }

        public IActionResult SubmitUpdate_Obsolete(ShopItem model, string shopId)
        {
            try
            {
                _shopService.CheckIfShopExists(shopId);
                _itemsService.SubmitDataAndUpdateDb(model, shopId, true);
                return RedirectToAction("AllItems");
            }
            catch
            {
                return RedirectToAction("UpdateItem", model);
            }
        }

        public IActionResult DeleteItem_Obsolete(ShopItem model)
        {
            _itemsService.DeleteItem(model);
            return RedirectToAction("AllItems");
        }
    }
}
