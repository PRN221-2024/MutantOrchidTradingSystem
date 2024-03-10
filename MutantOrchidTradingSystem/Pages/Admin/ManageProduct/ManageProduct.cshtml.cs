using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin
{
    public class ManageProductModel : PageModel
    {
        private readonly ProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ManageProductModel(ProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Product> listProducts { get; set; }
        public void OnGet()
        {
            listProducts = _productRepository.GetAll();
        }
    }
}
