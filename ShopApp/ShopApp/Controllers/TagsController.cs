using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos;
using ShopApp.Models;
using ShopApp.Services;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Controllers
{
    public class TagsController : Controller
    {
        private readonly TagsService _tagsService;
        private readonly ShopItemTagsService _shopItemTagsService;
        private readonly ValidationService _validationService;

        public TagsController(TagsService tagsService
            , ShopItemTagsService shopItemTagsService
            , ValidationService validationService)
        {
            _tagsService = tagsService;
            _shopItemTagsService = shopItemTagsService;
            _validationService = validationService;
        }

        public ActionResult All()
        {
            DisplayTags tagsInfo = new DisplayTags(_tagsService.GetAll());

            return View(tagsInfo);
        }

        public ActionResult Add()
        {
            CreateTag tagViewModel = new CreateTag();

            return View(tagViewModel);
        }

        [HttpPost]
        public ActionResult Add(CreateTag tagViewModel)
        {
            Tag tag = new Tag() { Name = tagViewModel.Name };
            List<object> allTags = _tagsService.GetAll().OfType<object>().ToList();
            bool uniqueTagName = _validationService.IsUnique(tag, allTags, "Name");

            TryValidateModel(tag);
            if(!uniqueTagName)
            {
                ModelState.AddModelError("Name", "Tag with this name already exists.");
            }

            if(!ModelState.IsValid)
            {
                return View(tagViewModel);
            }

            _tagsService.Create(tag);
            return RedirectToAction(nameof(All));
        }

        public ActionResult Update(int tagId)
        {
            Tag tag = _tagsService.GetById(tagId);
            return View(tag);
        }

        [HttpPost]
        public ActionResult Update(Tag tag)
        {
            List<object> allTags = _tagsService.GetAll().OfType<object>().ToList();
            bool uniqueTagName = _validationService.IsUnique(tag, allTags, "Name");

            if (!uniqueTagName)
            {
                ModelState.AddModelError("Name", "Tag with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(tag);
            }

            _tagsService.Update(tag);
            return RedirectToAction(nameof(All));
        }

        public ActionResult Delete(int tagId)
        {
            Tag delete = _tagsService.GetById(tagId);
            _shopItemTagsService.DeleteByTagId(tagId);
            _tagsService.Delete(delete);

            return RedirectToAction(nameof(All));
        }
    }
}
