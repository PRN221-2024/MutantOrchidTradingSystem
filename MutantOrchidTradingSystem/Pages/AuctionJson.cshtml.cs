using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json.Serialization;
using System.Text.Json;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.SignalR;

namespace MutantOrchidTradingSysRazorPage.Pages
{
    public class AuctionJsonModel : PageModel
    {
        private readonly IAuctionRepository _acutionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        public AuctionJsonModel(IAuctionRepository acutionRepository, IBidRepository bidRepository, IProductRepository productRepository, IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor, IHubContext<SignalServer> hubContext)
        {
            _acutionRepository = acutionRepository;
            _bidRepository = bidRepository;
            _productRepository = productRepository;
            _accountRepository = accountRepository;
           
        }

        public class BidDTO
        {
            public int Id { get; set; }
            public int AuctionId { get; set; }
            public int AccountId { get; set; }
            public decimal Amount { get; set; }
            public DateTime BidTime { get; set; }
            public string FullName { get; set; } // Add properties from Account that you need
            public string Address { get; set; } 
            
            // Add properties from Account that you need
        }

        public class AuctionDTO
        {
            public Auction Auction { get; set; }
            public List<BidDTO> Bids { get; set; }
            public Product Product { get; set; }
            public decimal CurrentBid { get; set; }
            public int BidCount { get; set; }
        }

        public JsonResult OnGetAuctionData(int id)
        {
            var auction = _acutionRepository.GetById(id);
            var product = _productRepository.GetById(auction.ProductId.Value);
            var bids = _bidRepository.GetBidsForAuction(id).Select(b => new BidDTO
            {
                Id = b.Id,
                AuctionId = b.AuctionId,
                AccountId = b.AccountId,
                Amount = b.Amount,
                BidTime = b.BidTime.Value,
                FullName = b.Account.FullName, // Map properties from Account
                Address = b.Account.Address    // Map properties from Account
            }).ToList();

            decimal maxBid = bids.Any() ? bids.Max(b => b.Amount) : auction.StartingPrice.Value;
            decimal currentBid = maxBid >= auction.StartingPrice.Value ? maxBid : auction.StartingPrice.Value;

            AuctionDTO auctionDto = new AuctionDTO
            {
                Auction = auction,
                Bids = bids,
                Product = product,
                CurrentBid = currentBid,
                BidCount = bids.Count
            };
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return new JsonResult(auctionDto, jsonSerializerOptions);
        }
    }
}
