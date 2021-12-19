using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using ShopApp.Services;

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
            return View(_itemsService.GetAllItems());
        }

        public IActionResult AddNewItem(Shop shopModel)
        {
            ShopItem emptyModel = new ShopItem();
            if (shopModel.Name != null)
            {
                ModelState.Clear(); // Be šito kai užkrauna View item name textbox rodo ne tuščia item name, bet shop name.
                emptyModel.Shop = _shopService.GetShop(shopModel);
            }
            return View(emptyModel);
        }

        [HttpPost]
        public IActionResult SubmitNewItem(ShopItem model, string shopId)
        {
            // Modelstate always return true. Even when model gets empty name. 
            try
            {
                if(!string.IsNullOrWhiteSpace(shopId))
                {
                    _shopService.CheckIfShopExists(shopId);
                }

                _itemsService.SubmitDataAndUpdateDb(model, shopId);

                if(!string.IsNullOrWhiteSpace(shopId))
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
        public IActionResult UpdateItem(ShopItem model)
        {
            return View(_itemsService.GetItem(model));
        }

        public IActionResult SubmitUpdate(ShopItem model, string shopId)
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

        public IActionResult DeleteItem(ShopItem model)
        {
            _itemsService.DeleteItem(model);
            return RedirectToAction("AllItems");
        }
    }
}
