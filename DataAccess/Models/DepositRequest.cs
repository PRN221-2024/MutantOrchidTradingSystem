using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class DepositRequest
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;
}
