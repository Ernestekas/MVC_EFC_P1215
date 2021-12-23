using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos;
using ShopApp.Models;
using ShopApp.Services;
using System.Linq;

namespace ShopApp.Controllers
{
    public class TagsController : Controller
    {
        private readonly ShopService _shopService;
        private readonly ItemsService _itemsService;
        private readonly TagsService _tagsService;
        public TagsController(ShopService shopService, ItemsService itemsService, TagsService tagsService)
        {
            _shopService = shopService;
            _itemsService = itemsService;
            _tagsService = tagsService;
        }
        // GET: TagsController
        public ActionResult All()
        {
            var tagsViewModel = new AllTagsViewModel(_tagsService.GetAll().ToList());

            return View(tagsViewModel);
        }

        public ActionResult Add()
        {
            TagViewModel emptyTag = new TagViewModel();

            return View(emptyTag);
        }


    }
}
