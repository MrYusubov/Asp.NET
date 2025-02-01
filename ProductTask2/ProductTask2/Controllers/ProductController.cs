using Microsoft.AspNetCore.Mvc;
using ProductTask2.Data;
using ProductTask2.Entities;
using ProductTask2.Models;

namespace ProductTask2.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list=new List<Product>();
            foreach (var item in _context.Products)
            {
                list.Add(item);
            }
            var vm = new ProductListViewmodel
            {
                Products = list
            };
            return View(vm);
        }
        public IActionResult Delete(int id)
        {
            _context.Products.Remove(_context.Products.FirstOrDefault(e => e.Id == id));
                TempData["Message"] = $"Product deleted successfully";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            var product = new Product();
            return View(product);
        }

        [HttpPost]
        public IActionResult Add(Product product, IFormFile ImageFile)
        {
            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(wwwRootPath))
            {
                Directory.CreateDirectory(wwwRootPath);
            }

            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            string filePath = Path.Combine(wwwRootPath, ImageName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }

            product.ImageLink = "/images/" + ImageName;
            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product, IFormFile ImageFile)
        {
            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(wwwRootPath))
            {
                Directory.CreateDirectory(wwwRootPath);
            }

            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            string filePath = Path.Combine(wwwRootPath, ImageName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }

            product.ImageLink = "/images/" + ImageName;
            _context.Products.Update(product);
            _context.SaveChanges();
                return RedirectToAction("Index");
        }
    }
}
