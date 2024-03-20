using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageProduct
{
    public class EditModel : PageModel
    {
        private readonly ProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public Product UpdatedProduct { get; set; }

        public EditModel(ProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet(int productId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            UpdatedProduct = _productRepository.GetById(productId);
            
            if (UpdatedProduct != null)
            {
                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            var updatedProduct = _productRepository.UpdateProduct(UpdatedProduct); 

            if(updatedProduct != null)
            {
                Console.WriteLine("Product updated successfully");
                return RedirectToPage("ManageProduct");
            }
            else
            {
                Console.WriteLine("Product not found or update fail!");
                return Page();
            }
            
        }
    }
}
