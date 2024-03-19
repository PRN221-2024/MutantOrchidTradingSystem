using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using System.Reflection;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class BidModel : PageModel
    {
        private readonly IAuctionRepository _acutionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<SignalServer> _bidHub;
        
        public BidModel(IAuctionRepository acutionRepository, IBidRepository bidRepository, IProductRepository productRepository, IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor, IHubContext<SignalServer> hubContext)
        {
            _acutionRepository = acutionRepository;
            _bidRepository = bidRepository;
            _productRepository = productRepository;
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
            _bidHub = hubContext;
        }
        public int BidCount { get; set; }
        public AuctionDTO AuctionDetail { get; set; }
        
       
       
        [BindProperty]
        public decimal amount { get; set; }
        public IActionResult OnGet(int id)
        {
            var auction = _acutionRepository.GetById(id);
            if (auction == null)
            {
                return NotFound();
            }
            Bid highestBid = new Bid();
            Account winner = new Account();
            List<Bid> bids = new List<Bid>();
            var product = _productRepository.GetById(auction.ProductId.Value);
            bids = _bidRepository.GetBidsForAuction(id);
            BidCount = bids.Count;
            decimal maxBid = bids.Any() ? bids.Max(b => b.Amount) : auction.StartingPrice.Value;
            decimal currentBid = maxBid >= auction.StartingPrice.Value ? maxBid : auction.StartingPrice.Value;
            if (bids != null)
            {
                highestBid = bids.OrderByDescending(b => b.Amount).FirstOrDefault();
            }


            if (highestBid != null)
            {
                winner = _accountRepository.GetById(highestBid.AccountId);
            }
            AuctionDTO auctionDto = new AuctionDTO
            {
                Auction = auction,
                Bids = bids,
                Product = product,
                CurrentBid = currentBid,
                Winner = winner,
            };
           AuctionDetail = auctionDto;

           

            return Page();
        }

        public async Task<IActionResult> OnPostBidAsync(int id)
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



            



            // Send the updated auction details to all connected clients
            await _bidHub.Clients.All.SendAsync("ReceiveBid", id);


            return RedirectToPage("/User/Bid", new { id = Bid.AuctionId });
        }

           //public IActionResult OnGetFindWinner(int id)
           // {
          
           //     var bids = _bidRepository.GetBidsForAuction(id);
           //     if (bids == null || bids.Count == 0)
           //     {
           //         // No bids were made on this auction
           //         return Page();
           //     }

           //     var highestBid = bids.OrderByDescending(b => b.Amount).FirstOrDefault();
           //     var winner = _accountRepository.GetById(highestBid.AccountId);

           //     // Return the winner's id
           //     if(winner.Id == _httpContextAccessor.HttpContext.Session.GetInt32("Id").Value)
           //     {
           //         return RedirectToPage("/User/WinningBid", new { id = highestBid.Auction.Id });
           //     }
           //     return Page();
            
           // }
    }
}
