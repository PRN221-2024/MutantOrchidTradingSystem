using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        private readonly FUMiniHotelManagementContext _context;

        public BookingReservationRepository()
        {
            _context = new FUMiniHotelManagementContext();
        }

        public BookingReservation GetById(int id)
        {
            return _context.BookingReservations.Find(id);
        }

        public List<BookingReservation> GetAll()
        {
            return _context.BookingReservations.ToList();
        }

        public void Add(BookingReservation bookingReservation)
        {
            _context.BookingReservations.Add(bookingReservation);
            _context.SaveChanges();
        }

        public void Update(BookingReservation bookingReservation)
        {
            _context.BookingReservations.Update(bookingReservation);
            _context.SaveChanges();
        }

        public void Delete(BookingReservation bookingReservation)
        {
            _context.BookingReservations.Remove(bookingReservation);
            _context.SaveChanges();
        }

        public List<BookingDetail> GetDataBookingByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.BookingDetails
                .Include(b => b.Room)
                .Include(b => b.BookingReservation.Customer)
                .Where(b => b.StartDate >= startDate && b.EndDate <= endDate)
                .OrderByDescending(b => b.ActualPrice)
                .ToList();
        }
        public List<BookingViewModel> GetBookingData()
        {
            try
            {
                var bookings = _context.BookingDetails
                .Include(b => b.Room)
                .ThenInclude(r => r.RoomType)
                .Include(b => b.BookingReservation)
                .Select(b => new BookingViewModel
                {
                    RoomId = b.Room.RoomId,
                    BookingReservationId = b.BookingReservationId,
                    CustomerId = b.BookingReservation.Customer.CustomerId,
                    CustomerFullName = b.BookingReservation.Customer.CustomerFullName,
                    RoomNumber = b.Room.RoomNumber,
                    RoomTypeName = b.Room.RoomType.RoomTypeName,
                    TotalPrice = b.BookingReservation.TotalPrice,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    ActualPrice = b.ActualPrice,
                    BookingDate = b.BookingReservation.BookingDate.HasValue ? b.BookingReservation.BookingDate.Value : new DateTime()
                })
                .ToList();
                return bookings ?? new List<BookingViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBookingData - BookingReservationRepository: {ex.Message}");
                throw;
            }
        }

        public List<BookingViewModel> GetBookingDataByUser(int userID)
        {
            try
            {
                var bookings = _context.BookingDetails
                .Where(b => b.BookingReservation.Customer.CustomerId == userID)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.BookingReservation)
                .Select(b => new BookingViewModel
                {
                    BookingReservationId = b.BookingReservationId,
                    RoomId = b.Room.RoomId,
                    CustomerId = b.BookingReservation.Customer.CustomerId,
                    CustomerFullName = b.BookingReservation.Customer.CustomerFullName,
                    RoomNumber = b.Room.RoomNumber,
                    RoomTypeName = b.Room.RoomType.RoomTypeName,
                    TotalPrice = b.BookingReservation.TotalPrice,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    ActualPrice = b.ActualPrice,
                    BookingDate = b.BookingReservation.BookingDate.HasValue ? b.BookingReservation.BookingDate.Value : new DateTime()
                })
                .ToList();

                return bookings ?? new List<BookingViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBookingDataByUser - BookingReservationRepository: {ex.Message}");
                throw;
            }
        }
        public List<BookingViewModel> GetDataBookingByRangeDate(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var bookings = _context.BookingDetails
                .Where(booking => booking.StartDate >= startDate && booking.EndDate <= endDate)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.BookingReservation)
                .Select(b => new BookingViewModel
                {
                    BookingReservationId = b.BookingReservationId,
                    RoomId = b.Room.RoomId,
                    CustomerId = b.BookingReservation.Customer.CustomerId,
                    CustomerFullName = b.BookingReservation.Customer.CustomerFullName,
                    RoomNumber = b.Room.RoomNumber,
                    RoomTypeName = b.Room.RoomType.RoomTypeName,
                    TotalPrice = b.BookingReservation.TotalPrice,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    ActualPrice = b.ActualPrice,
                    BookingDate = b.BookingReservation.BookingDate.HasValue ? b.BookingReservation.BookingDate.Value : new DateTime()
                })
                .ToList();
                return bookings ?? new List<BookingViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetDataBookingByRangeDate - BookingReservationRepository: {ex.Message}");
                throw;
            }
        }

        public bool CreateBooking(BookingViewModel model)
        {
            try
            {
                var (booking, bookingDetail) = ViewModelToModelConverter.ConvertToEntity(model);
                // Thêm mới booking
                if (booking.BookingReservationId > 0 && DoesBookingReservationExist(booking.BookingReservationId))
                {
                    _context.BookingDetails.Add(bookingDetail);
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    booking.BookingReservationId = GetMaxBookingReservationId() + 1;
                    _context.BookingReservations.Add(booking);
                    _context.SaveChanges();

                    // Lấy ID của booking vừa thêm mới
                    int bookingId = booking.BookingReservationId;

                    // Gán BookingReservationID cho BookingDetail và thêm mới chúng vào cơ sở dữ liệu
                    bookingDetail.BookingReservationId = bookingId;
                    _context.BookingDetails.Add(bookingDetail);

                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBooking - BookingReservationRepository: {ex.Message}");
                throw;
            }
        }

        public bool RemoveBookingDetails(BookingViewModel model)
        {
            try
            {
                var (booking, bookingDetail) = ViewModelToModelConverter.ConvertToEntity(model);
                _context.BookingDetails.Remove(bookingDetail);

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBooking - BookingReservationRepository: {ex.Message}");
                throw;
            }
        }

        public int GetMaxBookingReservationId()
        {
            var maxId = _context.BookingReservations
                                .OrderByDescending(b => b.BookingReservationId)
                                .Select(b => b.BookingReservationId)
                                .FirstOrDefault();
            return maxId;
        }

        public bool DoesBookingReservationExist(int bookingReservationId)
        {
            return _context.BookingReservations
                .Any(b => b.BookingReservationId == bookingReservationId);
        }
    }
    public class BookingReportModel
    {
        public string RoomNumber { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? ActualPrice { get; set; }
    }

    public class BookingViewModel
    {
        public int BookingReservationId { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerFullName { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomTypeName { get; set; }
        public decimal? TotalPrice { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
		public DateTime StartDate { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
		public DateTime EndDate { get; set; }
        public decimal? ActualPrice { get; set; }
        public DateTime BookingDate { get; set; }
    }

    public class ViewModelToModelConverter
    {
        public static (BookingReservation, BookingDetail) ConvertToEntity(BookingViewModel bookingViewModel)
        {
            BookingReservation bookingReservation;
            // Tạo mới đối tượng BookingReservation từ BookingViewModel
            if (bookingViewModel.BookingReservationId != -1)
            {
                bookingReservation = new BookingReservation
                {
                    BookingReservationId = bookingViewModel.BookingReservationId,
                    BookingDate = bookingViewModel.BookingDate,
                    TotalPrice = bookingViewModel.TotalPrice,
                    CustomerId = bookingViewModel.CustomerId,
                    BookingStatus = 1
                };
            }
            else
            {
                bookingReservation = new BookingReservation
                {
                    BookingDate = bookingViewModel.BookingDate,
                    TotalPrice = bookingViewModel.TotalPrice,
                    CustomerId = bookingViewModel.CustomerId,
                    BookingStatus = 1
                };
            }

            // Tạo mới danh sách BookingDetail từ RoomDetails của BookingViewModel
            var bookingDetail = new BookingDetail
            {
                BookingReservationId = bookingViewModel.BookingReservationId,
                RoomId = bookingViewModel.RoomId,
                StartDate = bookingViewModel.StartDate,
                EndDate = bookingViewModel.EndDate,
                ActualPrice = bookingViewModel.ActualPrice
            };

            return (bookingReservation, bookingDetail);
        }
    }

}
