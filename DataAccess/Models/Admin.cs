using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Admin
    {
        public Admin()
        {
            BuyerProblems = new HashSet<BuyerProblem>();
            SellerProblems = new HashSet<SellerProblem>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<BuyerProblem> BuyerProblems { get; set; }
        public virtual ICollection<SellerProblem> SellerProblems { get; set; }
    }
}
