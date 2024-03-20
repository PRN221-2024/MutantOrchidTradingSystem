using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Staff
{
    public class DeleteModel : PageModel
    {
        private readonly IAuctionRepository _acutionRepository;
        [BindProperty]
        public Auction Auction { get; set; }
        public DeleteModel(IAuctionRepository acutionRepository)
        {
            _acutionRepository = acutionRepository;
        }
        public IActionResult OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            Auction = _acutionRepository.GetById(id);
            if (Auction == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }

            var auctionId = Auction.Id;
            var existingAccount = _acutionRepository.GetById(auctionId);

            if(existingAccount.Bids.Count > 0)
            {
                TempData["DeleteError"] = "The auction has bids. It cannot be deleted.";
                return Page();
            }

            if (existingAccount != null)
            {
               _acutionRepository.DeleteAuction(auctionId);
                return RedirectToPage("/Staff/Auction");
            }
            else
            {
                return Page(); ;
            }
        }
    }
}
