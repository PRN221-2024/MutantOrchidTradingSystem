using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.ProductDetail
{
    public class ShopListModel : PageModel
    {
        public readonly IProductRepository _productRepository;

        public ShopListModel(ProductRepository productRepository)
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
