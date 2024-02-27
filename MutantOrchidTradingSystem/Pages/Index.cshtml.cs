using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductRepository _productRepository;
        public IndexModel(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public List<Product> listProducts { get; set; }
        public void OnGet()
        {
            listProducts = _productRepository.GetAll();
        }
    }
}
