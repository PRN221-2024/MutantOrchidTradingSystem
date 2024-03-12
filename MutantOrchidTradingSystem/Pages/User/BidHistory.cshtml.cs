using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class BidHistoryModel : PageModel
    {
        private readonly IBidRepository _bidRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BidHistoryModel(IBidRepository bidRepository, IHttpContextAccessor httpContextAccessor)
        {
            _bidRepository = bidRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<Bid> Bids { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            else
            {
                var accountId = (int)_httpContextAccessor.HttpContext.Session.GetInt32("Id");
                Bids = _bidRepository.GetListByaccountId(accountId);
                return Page();
            }
        }
    }
}
