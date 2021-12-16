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
        public ShopsController(ShopService shopService)
        {
            _shopService = shopService;
        }

        public IActionResult AllShops()
        {
            return View(_shopService.GetAllShops());
        }

        public IActionResult AddNewShop()
        {
            return View();
        }
    }
}
