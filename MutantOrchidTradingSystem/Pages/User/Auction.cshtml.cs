using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class AuctionModel : PageModel
    {
        private readonly IAuctionRepository _acutionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IProductRepository _productRepository;
        public List<AuctionDTO> Auctions { get; set; }
        public AuctionModel(IAuctionRepository acutionRepository, IBidRepository bidRepository, IProductRepository productRepository)
        {
            _acutionRepository = acutionRepository;
            _bidRepository = bidRepository;
            _productRepository = productRepository;

        }
        public void OnGet()
        {
            var auctionsFromRepo = _acutionRepository.GetAll();
            List<Bid> bidsForAuction = new List<Bid>();

            Auctions = new List<AuctionDTO>();

            foreach (var auction in auctionsFromRepo)
            {


                bidsForAuction = _bidRepository.GetBidsForAuction(auction.Id);
                var product = _productRepository.GetById(auction.ProductId.Value);

                decimal maxBid = bidsForAuction.Any() ? bidsForAuction.Max(b => b.Amount) : auction.StartingPrice.Value;
                decimal currentBid = maxBid >= auction.StartingPrice.Value ? maxBid : auction.StartingPrice.Value;


                AuctionDTO auctionDto = new AuctionDTO
                {
                    Auction = auction,
                    Bids = bidsForAuction,
                    Product = product,
                    CurrentBid = currentBid
                };
                Auctions.Add(auctionDto);
            }

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
