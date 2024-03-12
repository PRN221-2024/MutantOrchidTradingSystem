using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class AuctionDTO
    {
        public Auction Auction { get; set; }
        public Product Product { get; set; }
        public List<Bid> Bids { get; set; }

        public decimal CurrentBid { get; set; } = 0;
    }
}
