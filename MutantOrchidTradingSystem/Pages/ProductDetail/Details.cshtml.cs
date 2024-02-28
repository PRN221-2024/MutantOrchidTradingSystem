using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.ProductDetail
{
    public class DetailsModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public DetailsModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Product { get; set; }
        public IActionResult OnGet(int productId)
        {
            Product = _productRepository.GetById(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
