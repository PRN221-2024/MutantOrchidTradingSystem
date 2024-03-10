using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageProduct
{
    public class EditModel : PageModel
    {
        private readonly ProductRepository _productRepository;

        [BindProperty]
        public Product UpdatedProduct { get; set; }

        public EditModel(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult OnGet(int productId)
        {
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
