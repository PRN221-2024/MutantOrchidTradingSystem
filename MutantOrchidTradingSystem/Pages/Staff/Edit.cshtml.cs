using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace MutantOrchidTradingSysRazorPage.Pages.Staff
{
    public class EditModel : PageModel
    {
        private readonly IAuctionRepository _acutionRepository;
        private readonly IProductRepository _productRepository;
        private readonly IHubContext<SignalServer> _bidHub;
        [BindProperty]
        public Auction Auction { get; set; }
        public List<SelectListItem> ProductOptions { get; set; }
        public EditModel(IAuctionRepository acutionRepository, IProductRepository productRepository, IHubContext<SignalServer> bidHub)
        {
            _acutionRepository = acutionRepository;
            _productRepository = productRepository;
            _bidHub = bidHub;
        }
        public IActionResult OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            var products = _productRepository.GetAll();
            ProductOptions = new List<SelectListItem>();
            Auction = _acutionRepository.GetById(id);




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
            var products = _productRepository.GetAll();

            ProductOptions = new List<SelectListItem>();

            foreach (var product in products)
            {
                ProductOptions.Add(new SelectListItem { Value = product.Id.ToString(), Text = product.Name });
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Auction.StartTime > Auction.EndTime)
            {
                ModelState.AddModelError("Auction.EndTime", "End time must be greater than current time");
                return Page();
            }
            try
            {
                _acutionRepository.UpdateAuction(Auction);
                return RedirectToPage("Auction");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPost - CreateAuctionModel: {ex.Message}");
                return Page();
            }
        }
    }
}
