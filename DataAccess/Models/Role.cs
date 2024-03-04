using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Status { get; set; }
}
