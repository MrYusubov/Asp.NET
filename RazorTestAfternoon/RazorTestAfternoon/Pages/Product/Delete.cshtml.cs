using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorTestAfternoon.Data;

namespace RazorTestAfternoon.Pages.Product
{
    public class DeleteModel : PageModel
    {
        public readonly ProductDbContext _context;
        public DeleteModel(ProductDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Entities.Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _context.Products.FirstOrDefault(Product => Product.Id == id);
        }
        public IActionResult OnPost() 
        {
            _context.Remove(Product);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
