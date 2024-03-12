using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Bid
{
    public int Id { get; set; }

    public int AuctionId { get; set; }

    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? BidTime { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Auction? Auction { get; set; }
}
