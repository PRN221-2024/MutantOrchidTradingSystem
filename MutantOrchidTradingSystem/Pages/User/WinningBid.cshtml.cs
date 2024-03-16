using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class WinningBidModel : PageModel
    {
        private readonly IBidRepository _bidRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WinningBidModel(IBidRepository bidRepository, IAuctionRepository auctionRepository, IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _bidRepository = bidRepository;
            _auctionRepository = auctionRepository;
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public Bid highestBid { get; set; }
        public IActionResult OnGet(int id)
        {
             string username = _httpContextAccessor.HttpContext.Session.GetString("username");
            if (username == null)
            {
                return RedirectToPage("/Login");
            }
            var bids = _bidRepository.GetBidsForAuction(id);
            if (bids == null || bids.Count == 0)
            {
                // No bids were made on this auction
                return Page();
            }
            highestBid = bids.OrderByDescending(b => b.Amount).FirstOrDefault();
            

            return Page();
        }
    }
}
