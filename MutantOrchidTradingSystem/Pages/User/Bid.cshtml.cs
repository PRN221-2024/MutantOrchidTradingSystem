using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class BidModel : PageModel
    {
        private readonly IAuctionRepository _acutionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public BidModel(IAuctionRepository acutionRepository, IBidRepository bidRepository, IProductRepository productRepository, IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _acutionRepository = acutionRepository;
            _bidRepository = bidRepository;
            _productRepository = productRepository;
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public int BidCount { get; set; }
        public AuctionDTO AuctionDetail { get; set; }
        public List<Account> accounts { get; set; }
        public List<Bid> listBid { get; set; }
       
        [BindProperty]
        public decimal amount { get; set; }
        public IActionResult OnGet(int id)
        {
            var auction = _acutionRepository.GetById(id);
            if (auction == null)
            {
                return NotFound();
            }
            Account accountBid = new Account();
            var product = _productRepository.GetById(auction.ProductId.Value);
            var bids = _bidRepository.GetBidsForAuction(id);
            listBid = bids; 
            BidCount = bids.Count;
            decimal maxBid = bids.Any() ? bids.Max(b => b.Amount) : auction.StartingPrice.Value;
            decimal currentBid = maxBid >= auction.StartingPrice.Value ? maxBid : auction.StartingPrice.Value;

            AuctionDTO auctionDto = new AuctionDTO
            {
                Auction = auction,
                Bids = bids,
                Product = product,
                CurrentBid = currentBid
            };
           AuctionDetail = auctionDto;

           

            return Page();
        }

        public IActionResult OnPostBid(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            Bid Bid = new Bid();
            Bid.AuctionId = id;
            Bid.AccountId = _httpContextAccessor.HttpContext.Session.GetInt32("Id").Value;
            Bid.BidTime = DateTime.Now;
            Bid.Amount = amount;
            var bid = _bidRepository.AddBid(Bid);
            return RedirectToPage("/User/Bid", new { id = Bid.AuctionId });
        }
    }
}
