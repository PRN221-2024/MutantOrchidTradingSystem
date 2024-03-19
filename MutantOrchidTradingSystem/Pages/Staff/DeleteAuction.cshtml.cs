using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Staff
{
    public class DeleteAuctionModel : PageModel
    {
        private readonly IAuctionRepository _auctionRepository;

        public DeleteAuctionModel(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }
        public void OnGet(int id)
        {

        }
    }
}
