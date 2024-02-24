using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public interface IBookingReservationRepository
    {
        public BookingReservation GetById(int id);
        public List<BookingReservation> GetAll();
        public void Add(BookingReservation bookingReservation);
        public void Update(BookingReservation bookingReservation);
        public void Delete(BookingReservation bookingReservation);
        public List<BookingViewModel> GetBookingData();
        public List<BookingViewModel> GetBookingDataByUser(int userID);
        public List<BookingViewModel> GetDataBookingByRangeDate(DateTime? startDate, DateTime? endDate);
        public bool CreateBooking(BookingViewModel model);
        public bool RemoveBookingDetails(BookingViewModel model);
        public List<BookingDetail> GetDataBookingByDateRange(DateTime startDate, DateTime endDate);

    }
}
