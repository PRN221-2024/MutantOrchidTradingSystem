using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageProduct
{
    public class DeleteModel : PageModel
    {
        private readonly ProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteModel(ProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public Product Product { get; set; }    

        public IActionResult OnGet(int productId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            Product = _productRepository.GetById(productId);

            if(Product == null)
            {
                return RedirectToPage("./ManageProduct");
            }
            else
            {
                return Page();
            }
        }


        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            var productId = Product.Id;
            var existingProduct = _productRepository.GetById(productId);

            if (existingProduct != null)
            {
                _productRepository.DeleteProduct(productId);
                return RedirectToPage("ManageProduct");
            }
            else
            {
                return Page(); ;
            }
        }
    }
}
