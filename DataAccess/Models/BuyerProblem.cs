using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BuyerProblem
    {
        public int Id { get; set; }
        public int? AdminId { get; set; }
        public int? BuyerId { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual Buyer? Buyer { get; set; }
    }
}
