﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Photo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
