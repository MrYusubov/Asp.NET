using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCart.Entities;
using ProductCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductCart.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> Products = new List<Product> {
            new Product
            {
                Id = 1,
                Name = "Pendir",
                Description="Labne",
                Price=6.5,
                Discount=15
            },
            new Product
            {
                Id = 2,
                Name = "Yag",
                Description="Nehre yagi",
                Price=14.00,
                Discount=5
            },
            new Product
            {
                Id = 3,
                Name = "Qaymaq",
                Description="Inek Sudunden Tebii Qaymaq",
                Price=8.99,
                Discount=7
            }
        };
        public IActionResult Index()
        {
            return View(Products);
        }
        public IActionResult Delete(int id)
        {
            var item = Products.FirstOrDefault(e => e.Id == id);
            if (item != null)
            {
                Products.Remove(item);
                TempData["Message"] = $"{item.Name} deleted successfully";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            var product=new Product();
            return View(product);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid) 
            {
                product.Id= (new Random()).Next(1, 10000);
                Products.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Update(int id) 
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                Products.Remove(Products.FirstOrDefault(p => p.Id == product.Id));
                Products.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
