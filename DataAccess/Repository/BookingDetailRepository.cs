using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {

        private readonly FUMiniHotelManagementContext _context;

        public BookingDetailRepository()
        {
            _context = new FUMiniHotelManagementContext();
        }
        public BookingDetail GetById(int bookingReservationId, int roomId)
        {
            return _context.BookingDetails.Find(bookingReservationId, roomId);
        }

        public List<BookingDetail> GetAll()
        {
            return _context.BookingDetails.ToList();
        }

        public void Add(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Add(bookingDetail);
            _context.SaveChanges();
        }

        public void Update(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Update(bookingDetail);
            _context.SaveChanges();
        }

        public void Delete(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Remove(bookingDetail);
            _context.SaveChanges();
        }

        public List<RoomInformation> GetAllRoomInBookingDetail()
        {
            return _context.BookingDetails.Select(i=>i.Room).ToList();
        }
    }
}
