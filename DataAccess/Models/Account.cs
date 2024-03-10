using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Account
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public bool? Status { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RoleAccount> RoleAccounts { get; set; } = new List<RoleAccount>();
}
