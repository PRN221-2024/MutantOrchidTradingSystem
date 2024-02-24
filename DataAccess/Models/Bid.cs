using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Bid
    {
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public int? BuyerId { get; set; }
        public decimal? BidAmount { get; set; }
        public DateTime? BidTime { get; set; }
        public bool? WinStatus { get; set; }

        public virtual Buyer? Buyer { get; set; }
        public virtual Item? Item { get; set; }
    }
}
