using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public decimal? TotalPrice { get; set; }

    public bool? Payed { get; set; }

    public bool? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
