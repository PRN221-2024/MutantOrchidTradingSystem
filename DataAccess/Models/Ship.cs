using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Ship
    {
        public int Id { get; set; }
        public decimal? ShipPrice { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
    }
}
