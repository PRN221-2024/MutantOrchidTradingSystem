using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MutantOrchidTradingSysRazorPage.Pages.Staff
{
    public class CreateAuctionModel : PageModel
    {
        private readonly IAuctionRepository _acutionRepository;
        private readonly IProductRepository _productRepository;
        [BindProperty]
        public Auction Auction { get; set; }
        public List<SelectListItem> ProductOptions { get; set; }
        public CreateAuctionModel(IAuctionRepository acutionRepository, IProductRepository productRepository)
        {
            _acutionRepository = acutionRepository;
            _productRepository = productRepository;
        }
        public IActionResult OnGet()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            var products = _productRepository.GetAll();
            ProductOptions = new List<SelectListItem>();
            Auction = new Auction(); 
            Auction.StartTime = DateTime.Now;
           
           
            
            foreach (var product in products)
            {
                ProductOptions.Add(new SelectListItem { Value = product.Id.ToString(), Text = product.Name });
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
            Auction.Status = true;
            var selectedProductId = Auction.ProductId;
            _acutionRepository.Create(Auction);


            return Page();
        }
    }
}
