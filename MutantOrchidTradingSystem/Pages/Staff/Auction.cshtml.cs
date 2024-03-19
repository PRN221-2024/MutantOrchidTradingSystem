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
        private readonly IHttpContextAccessor _contextAccessor;
        public AuctionModel(IAuctionRepository acutionRepository, IHttpContextAccessor httpContextAccessor)
        {
            _acutionRepository = acutionRepository;
            _contextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet()
        {
            if(_contextAccessor.HttpContext.Session.GetString("username") == null)
            {
                return Redirect("/Login");
            }
            Auctions = _acutionRepository.GetAll();
            return Page();
        }
    }
}
