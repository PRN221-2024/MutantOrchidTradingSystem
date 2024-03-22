using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class DeductionRequest
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Account Account { get; set; } = null!;
}
