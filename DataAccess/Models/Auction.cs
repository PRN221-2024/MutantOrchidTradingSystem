using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Auction
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public decimal? StartingPrice { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual Product? Product { get; set; }
}
