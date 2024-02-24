using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository;

namespace BusinessObject
{
    public class BookingObject
    {
        private readonly IBookingReservationRepository _reservationRepository;
        public BookingObject()
        {
            _reservationRepository = new BookingReservationRepository();
        }
        public List<BookingReservation> GetAllBookingReser() => _reservationRepository.GetAll();
        public List<BookingViewModel> GetAllDataBooking() => _reservationRepository.GetBookingData();
        public List<BookingViewModel> GetDataBookingUser(int userId) => _reservationRepository.GetBookingDataByUser(userId);
        public List<BookingViewModel> GetDataBookingByRangeDate(DateTime? startDate, DateTime? endDate) => _reservationRepository.GetDataBookingByRangeDate(startDate,endDate);
        public bool CreateBooking(BookingViewModel model) => _reservationRepository.CreateBooking(model);
        public bool DeleteBooking(BookingViewModel model) => _reservationRepository.RemoveBookingDetails(model);
        public bool IsDuplicateBooking(int bookingId, int roomId)
        {
            using (var context = new FUMiniHotelManagementContext())  // Thay YourDbContext bằng DbContext thực tế của bạn
            {
                // Kiểm tra xem có bản ghi nào có cùng BookingReservationId và RoomId hay không
                var existingBooking = context.BookingDetails
                    .FirstOrDefault(b => b.BookingReservationId == bookingId && b.RoomId == roomId);

                // Nếu tồn tại, trả về true (trùng); ngược lại, trả về false (không trùng)
                return existingBooking != null;
            }
        }
        public List<BookingReportModel> GetDataBookingReport(DateTime startDate, DateTime endDate)
        {
            var bookings = _reservationRepository.GetDataBookingByDateRange(startDate, endDate);

            var bookingReport = bookings.Select(b => new BookingReportModel
            {
                RoomNumber = b.Room.RoomNumber,
                CustomerFullName = b.BookingReservation.Customer.CustomerFullName,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                ActualPrice = b.ActualPrice
            }).ToList();

            return bookingReport;
        }
    }
}
