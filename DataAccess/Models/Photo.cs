using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Photo
    {
        public string? Photo1 { get; set; }
        public int? ItemId { get; set; }

        public virtual Item? Item { get; set; }
    }
}
