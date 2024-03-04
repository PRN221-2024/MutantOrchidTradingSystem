using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public string? Path { get; set; }

    public string? FileName { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
