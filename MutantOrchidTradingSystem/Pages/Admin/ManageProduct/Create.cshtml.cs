using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageProduct
{
    public class CreateModel : PageModel
    {
        private readonly ProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public Product NewProduct { get; set; }
        public CreateModel(ProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                List<string> existedProductName = _productRepository.GetProductName();
                var validationResult = NewProduct.ProductNameNotIdentical(existedProductName);
                if (validationResult != ValidationResult.Success)
                {
                    TempData["ProductnameError"] = validationResult.ErrorMessage;
                    return Page();
                }
                _productRepository.CreateProduct(NewProduct);
                return RedirectToPage("./ManageProduct");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPost - CreateModel: {ex.Message}");
                return Page();
            }
        }

    }
}
