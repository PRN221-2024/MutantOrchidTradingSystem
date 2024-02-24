using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BuyerFeedback
    {
        public int Id { get; set; }
        public int? Rating { get; set; }
        public DateTime? Date { get; set; }
        public string? Comment { get; set; }
        public int? BuyerId { get; set; }
        public int? SellerId { get; set; }

        public virtual Buyer? Buyer { get; set; }
        public virtual Seller? Seller { get; set; }
    }
}
