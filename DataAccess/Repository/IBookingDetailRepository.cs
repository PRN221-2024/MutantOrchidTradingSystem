using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public interface IBookingDetailRepository
    {
        public BookingDetail GetById(int bookingReservationId, int roomId);
        public List<BookingDetail> GetAll();
        public void Add(BookingDetail bookingDetail);
        public void Update(BookingDetail bookingDetail);
        public void Delete(BookingDetail bookingDetail);
        public List<RoomInformation> GetAllRoomInBookingDetail();
    }
}
