using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorTestAfternoon.Data;
using RazorTestAfternoon.Entities;

namespace RazorTestAfternoon.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly ProductDbContext _context;
        public EditModel(ProductDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Entities.Product Product { get; set; }
        public void OnGet(int id)
        {
            Product=_context.Products.FirstOrDefault(Product => Product.Id == id);
        }
        public IActionResult OnPost() 
        {
            _context.Update(Product);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
