using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Staff
{
    public class AuctionModel : PageModel
    {
        private readonly IAuctionRepository _acutionRepository;
        public List<Auction> Auctions { get; set; }
        public AuctionModel(IAuctionRepository acutionRepository)
        {
            _acutionRepository = acutionRepository;
        }
        public void OnGet()
        {
            Auctions = _acutionRepository.GetAll();
        }
    }
}
