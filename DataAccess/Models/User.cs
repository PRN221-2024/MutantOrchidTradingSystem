using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            Admins = new HashSet<Admin>();
            Sellers = new HashSet<Seller>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? UserType { get; set; }

        public virtual UserType? UserTypeNavigation { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<Seller> Sellers { get; set; }
    }
}
