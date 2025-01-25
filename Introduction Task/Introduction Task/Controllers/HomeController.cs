using System.Collections.Generic;
using System.Diagnostics;
using Introduction_Task.Entities;
using Introduction_Task.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Introduction_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public List<Drink> drinks = new List<Drink>
        {
            new Drink
            {
                Id = 1,
                Name="Coca-Cola",
                Price=2.20
            },
            new Drink
            {
                Id = 2,
                Name="Bizon",
                Price=0.8
            },
            new Drink
            {
                Id = 3,
                Name="Sprite",
                Price=2.10
            }
        };
        public List<FastFood> fastFoods = new List<FastFood>
        {
            new FastFood
            {
                Id = 1,
                Name="Burger",
                Price=9.99
            },
            new FastFood
            {
                Id = 2,
                Name="Pizza",
                Price=23
            },
            new FastFood
            {
                Id = 3,
                Name="Nuggets",
                Price=5.4
            }
        };
        public List<HotMeal> hotMeals = new List<HotMeal>
        {
            new HotMeal
            {
                Id = 1,
                Name="Plov",
                Price=4.20
            },
            new HotMeal
            {
                Id = 2,
                Name="Dolma",
                Price=7.50
            },
            new HotMeal
            {
                Id = 3,
                Name="Pide",
                Price=3.20
            }
        };
        public IActionResult Drinks()
        {
            return View(drinks);
        }
        public IActionResult FastFoods()
        {
            return View(fastFoods);
        }
        public IActionResult HotMeals()
        {
            return View(hotMeals);
        }
        public IActionResult Index()
        {
            var vm = new IndexViewModel
            {
                HotMeal = hotMeals,
                Drink=drinks,
                FastFood=fastFoods
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
