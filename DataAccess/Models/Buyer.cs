using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Buyer
    {
        public Buyer()
        {
            Bids = new HashSet<Bid>();
            BuyerCommunications = new HashSet<BuyerCommunication>();
            BuyerFeedbacks = new HashSet<BuyerFeedback>();
            BuyerProblems = new HashSet<BuyerProblem>();
            Items = new HashSet<Item>();
            SellerCommunications = new HashSet<SellerCommunication>();
            SellerFeedbacks = new HashSet<SellerFeedback>();
            SellerProblems = new HashSet<SellerProblem>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<BuyerCommunication> BuyerCommunications { get; set; }
        public virtual ICollection<BuyerFeedback> BuyerFeedbacks { get; set; }
        public virtual ICollection<BuyerProblem> BuyerProblems { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<SellerCommunication> SellerCommunications { get; set; }
        public virtual ICollection<SellerFeedback> SellerFeedbacks { get; set; }
        public virtual ICollection<SellerProblem> SellerProblems { get; set; }
    }
}
