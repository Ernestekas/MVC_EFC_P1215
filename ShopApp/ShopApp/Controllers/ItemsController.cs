using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using ShopApp.Services;

namespace ShopApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemsService _itemsService;
        public ItemsController(ItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        public IActionResult AllItems()
        {
            return View(_itemsService.GetAllItems());
        }

        public IActionResult AddNewItem(ShopItem model)
        {
            return View(new ShopItem());
        }

        [HttpPost]
        public IActionResult SubmitNewItem(ShopItem model, string shopId)
        {
            try
            {
                _itemsService.SubmitNewItemToDb(model, shopId);
                return RedirectToAction("AllItems");
            }
            catch
            {
                return RedirectToAction("AddNewItem", model);
            }
        }
        public IActionResult UpdateItem(ShopItem model)
        {
            //_itemsService.SubmitUpdateItemToDb(model, shopId);
            Shop test = model.Shop;
            return View(model);
        }
    }
}
