using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Item
    {
        public Item()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Condition { get; set; }
        public string? Category { get; set; }
        public decimal? StartBidAmount { get; set; }
        public DateTime? AuctionStart { get; set; }
        public DateTime? AuctionEnd { get; set; }
        public decimal? ShipPrice { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string? AccountType { get; set; }
        public string? AccountNo { get; set; }
        public int? SellerId { get; set; }
        public int? BuyerId { get; set; }

        public virtual Buyer? Buyer { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
