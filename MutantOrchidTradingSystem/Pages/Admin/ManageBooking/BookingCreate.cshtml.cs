using BusinessObject;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageBooking
{
    public class BookingCreateModel : PageModel
    {
        private readonly BookingObject _bookingObject;

        public BookingCreateModel(BookingObject bookingObject)
        {
            _bookingObject = bookingObject;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("account") is null)
            {
                return RedirectToPage("/Login");
            }

            var role = HttpContext.Session.GetString("account");

            if (role != "admin")
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }

        [BindProperty]
        public BookingViewModel booking { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _bookingObject.CreateBooking(booking);
            return Redirect("./ListCustomer");
        }
    }
}
