using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Customer
    {
        public Customer()
        {
            BookingReservations = new HashSet<BookingReservation>();
        }

        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public string? CustomerFullName { get; set; }
        [Required(ErrorMessage = "Telephone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Telephone { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmailAddress { get; set; } = null!;
        [Required(ErrorMessage = "Birth day is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? CustomerBirthday { get; set; }
        public byte? CustomerStatus { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public virtual ICollection<BookingReservation> BookingReservations { get; set; }
    }
}
