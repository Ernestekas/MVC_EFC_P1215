using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos;
using ShopApp.Dtos.Items;
using ShopApp.Models;
using ShopApp.Services;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemsService _itemsService;
        private readonly ShopService _shopService;
        private readonly TagsService _tagsService;
        private readonly ValidationService _validationService;
        private readonly ShopItemTagsService _shopItemTagsService;

        public ItemsController
            (ItemsService itemsService
            , ShopService shopService
            , TagsService tagsService
            , ValidationService validationService
            , ShopItemTagsService shopItemTagsService)
        {
            _itemsService = itemsService;
            _shopService = shopService;
            _tagsService = tagsService;
            _validationService = validationService;
            _shopItemTagsService = shopItemTagsService;
        }

        public IActionResult All()
        {
            List<ShopItem> model = _itemsService.GetAll();

            return View(model);
        }

        public IActionResult All_InProgress()
        {
            List<DisplayItem> itemsVM = new List<DisplayItem>();
            foreach(var item in _itemsService.GetAll())
            {
                List<int> tagsIds = _shopItemTagsService.GetTagsByItemId(item.Id);
                List<Tag> tags = _tagsService.GetByIdList(tagsIds);

                DisplayItem itemVM = new DisplayItem(item, tags);

                if(item.Shop != null)
                {
                    itemVM.ShopId = item.Shop.Id;
                    itemVM.ShopName = item.Shop.Name;
                }

                itemsVM.Add(itemVM);
            }

            return View(itemsVM);
        }

        public IActionResult Add_InProgress()
        {
            CreateItem itemVM = new CreateItem() { Tags = _tagsService.GetAll()};
            return View(itemVM);
        }

        [HttpPost]
        public IActionResult Add_InProgress(CreateItem itemVM)
        {
            ShopItem item = itemVM.GetItem(_shopService.GetById(itemVM.ShopId));
            List<object> items = _itemsService.GetAll().OfType<object>().ToList();
            bool uniqueItemName = _validationService.IsUnique(item, items, "Name");

            TryValidateModel(item);
            if (!uniqueItemName)
            {
                ModelState.AddModelError("Name", "Item with this name already exists.");
            }

            if(!ModelState.IsValid)
            {
                itemVM.Tags = _tagsService.GetAll();
                return View(itemVM);
            }

            _itemsService.Create(item);

            item.Id = _itemsService.GetByName(item.Name).Id;

            _shopItemTagsService.Create(item.Id, itemVM.SelectedTagsIds);
            
            return RedirectToAction(nameof(All_InProgress));
        }

        public IActionResult Add()
        {
            return View(new ShopItem());
        }

        [HttpPost]
        public IActionResult Add(ShopItem item, int shopId)
        {
            List<object> items = _itemsService.GetAll().OfType<object>().ToList();
            bool uniqueItemName = _validationService.IsUnique(item, items, "Name");

            if(!uniqueItemName)
            {
                ModelState.AddModelError("Name", "Item with this name already exists.");
            }

            if(!ModelState.IsValid)
            {
                return View(item);
            }

            Shop shop = _shopService.GetById(shopId);
            _itemsService.CreateOrUpdate(item, shop);

            return RedirectToAction(nameof(All));
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

        public IActionResult Update_InProgress(DisplayItem itemVM)
        {
            List<int> tagsIds = _shopItemTagsService.GetTagsByItemId(itemVM.Id);
            List<Tag> tags = _tagsService.GetByIdList(tagsIds);

            itemVM.ItemTags = string.Join(", ", tags);

            UpdateItem itemUpdateVM = new UpdateItem(itemVM)
            {
                Tags = _tagsService.GetAll()
            };

            return View(itemUpdateVM);
        }

        [HttpPost]
        public IActionResult Update_InProgress(UpdateItem itemVM)
        {
            ShopItem item = _itemsService.GetById(itemVM.Id);
            item.Name = itemVM.Name;
            item.ExpiryDate = itemVM.ExpiryDate;
            item.Shop = _shopService.GetById(itemVM.ShopId);
            bool uniqueName = _validationService.IsUnique(item, _itemsService.GetAll().OfType<object>().ToList(), "Name");

            TryValidateModel(item);
            if(!uniqueName)
            {
                ModelState.AddModelError("Name", "Item with this name already exists.");
            }

            if(!ModelState.IsValid)
            {
                List<int> tagsIds = _shopItemTagsService.GetTagsByItemId(item.Id);
                List<Tag> tags = _tagsService.GetByIdList(tagsIds);

                DisplayItem errorItemVM = new DisplayItem(item, tags);

                return View(errorItemVM);
            }

            _itemsService.Update(item);
            _shopItemTagsService.DeleteByItemId(item.Id);
            _shopItemTagsService.Create(item.Id, itemVM.SelectedTagsIds);

            return RedirectToAction(nameof(All_InProgress));
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

                return RedirectToAction(nameof(All));
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
            _shopItemTagsService.DeleteByItemId(model.Id);
            _itemsService.Delete(model);

            return RedirectToAction(nameof(All));
        }
    }
}
