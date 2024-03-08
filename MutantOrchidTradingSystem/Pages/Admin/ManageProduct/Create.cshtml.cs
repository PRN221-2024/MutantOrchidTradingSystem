using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageProduct
{
    public class CreateModel : PageModel
    {
        private readonly ProductRepository _productRepository;

        [BindProperty]
        public Product NewProduct { get; set; }
        public CreateModel(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _productRepository.CreateProduct(NewProduct);
                return RedirectToPage("./ManageProduct");
            }catch(Exception ex)
            {
                Console.WriteLine($"Error in OnPost - CreateModel: {ex.Message}");
                return Page();
            }
        }
    }
}
