using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Seller
    {
        public Seller()
        {
            BuyerCommunications = new HashSet<BuyerCommunication>();
            BuyerFeedbacks = new HashSet<BuyerFeedback>();
            Items = new HashSet<Item>();
            SellerCommunications = new HashSet<SellerCommunication>();
            SellerFeedbacks = new HashSet<SellerFeedback>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<BuyerCommunication> BuyerCommunications { get; set; }
        public virtual ICollection<BuyerFeedback> BuyerFeedbacks { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<SellerCommunication> SellerCommunications { get; set; }
        public virtual ICollection<SellerFeedback> SellerFeedbacks { get; set; }
    }
}
