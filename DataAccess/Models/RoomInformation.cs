using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class RoomInformation
    {
        public RoomInformation()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public int RoomId { get; set; }
        [Required(ErrorMessage = "RoomNumber is required")]
        public string RoomNumber { get; set; } = null!;
        [Required(ErrorMessage = "RoomDetailDescription is required")]
        public string? RoomDetailDescription { get; set; }
        [Required(ErrorMessage = "RoomMaxCapacity is required")]
        public int? RoomMaxCapacity { get; set; }
        [Required(ErrorMessage = "RoomTypeId is required")]
        public int RoomTypeId { get; set; }
        public byte? RoomStatus { get; set; }
        [Required(ErrorMessage = "RoomPricePerDay is required")]
        public decimal? RoomPricePerDay { get; set; }

        public virtual RoomType RoomType { get; set; } = null!;
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }

        public override int GetHashCode()
        {
            return RoomId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            RoomInformation otherRoom = (RoomInformation)obj;
            return RoomId == otherRoom.RoomId;
        }
    }
    public class RoomInformationEqualityComparer : IEqualityComparer<RoomInformation>
    {
        public bool Equals(RoomInformation x, RoomInformation y)
        {
            // Check if RoomInformation objects have the same RoomId
            return x.RoomId == y.RoomId;
        }

        public int GetHashCode(RoomInformation obj)
        {
            // Return the hash code of the RoomInformation's RoomId
            return obj.RoomId.GetHashCode();
        }
    }
}
