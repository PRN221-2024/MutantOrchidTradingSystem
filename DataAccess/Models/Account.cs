using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            RoleAccounts = new HashSet<RoleAccount>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public bool? Status { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
    }
}
