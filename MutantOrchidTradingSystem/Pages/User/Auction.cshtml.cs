using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging.Signing;
using System.Reflection;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class AuctionModel : PageModel
    {
        private readonly IAuctionRepository _acutionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        public List<AuctionDTO> Auctions { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public AuctionModel(IAuctionRepository acutionRepository, IBidRepository bidRepository, IProductRepository productRepository, IAccountRepository accountRepository)
        {
            _acutionRepository = acutionRepository;
            _bidRepository = bidRepository;
            _productRepository = productRepository;
            _accountRepository = accountRepository;
        }
        public IActionResult OnGet(int id)
        {
            int pageSize = 3;
            var auctionsFromRepo = _acutionRepository.GetAll();
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(auctionsFromRepo.Count / (double)PageSize);
            CurrentPage = id;
            int startIndex = (CurrentPage - 1) * PageSize;
            var auctionsForPage = auctionsFromRepo.Skip(startIndex).Take(PageSize).ToList();
            Auctions = new List<AuctionDTO>();
            
            foreach (var auction in auctionsForPage)
            {

                Bid highestBid = new Bid();
                Account winner = new Account();
                List<Bid> bidsForAuction = new List<Bid>();
                bidsForAuction = _bidRepository.GetBidsForAuction(auction.Id);
                var product = _productRepository.GetById(auction.ProductId.Value);
                if(bidsForAuction != null)
                {
                    highestBid = bidsForAuction.OrderByDescending(b => b.Amount).FirstOrDefault();
                }
                
                
                if (highestBid != null)
                {
                     winner =  _accountRepository.GetById(highestBid.AccountId);
                }
               
                decimal maxBid = bidsForAuction.Any() ? bidsForAuction.Max(b => b.Amount) : auction.StartingPrice.Value;
                decimal currentBid = maxBid >= auction.StartingPrice.Value ? maxBid : auction.StartingPrice.Value;


                AuctionDTO auctionDto = new AuctionDTO
                {
                    Auction = auction,
                    Bids = bidsForAuction,
                    Product = product,
                    CurrentBid = currentBid,
                    Winner = winner
                };
                Auctions.Add(auctionDto);
            }
            return Page();
        }

        public IActionResult OnGetUpdate(int id)
        {
            var auction = _acutionRepository.GetById(id);
            if (auction != null)
            {

                _acutionRepository.UpdateStatusAuction(id); 
                return RedirectToPage("/User/Auction"); 
            }
            else
            {
                return NotFound();
            }
        }
        
    }
}
