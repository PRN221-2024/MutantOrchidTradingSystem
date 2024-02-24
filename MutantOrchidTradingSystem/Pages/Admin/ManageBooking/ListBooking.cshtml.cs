using BusinessObject;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageBooking
{
    public class ListBookingModel : PageModel
    {
		public IList<BookingViewModel> Booking { get; set; }
		[BindProperty] public string? Keyword { get; set; }
		[BindProperty(SupportsGet = true)] public DateTime? StartDate { get; set; }
		[BindProperty(SupportsGet = true)] public DateTime? EndDate { get; set; }


		public bool isSearch = false;

		private readonly BookingObject _bookingObject;
		public ListBookingModel(BookingObject _bookingObject)
		{
			this._bookingObject = _bookingObject;
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
			Booking = _bookingObject.GetAllDataBooking();
			return Page();
		}
		public async Task OnPost()
		{
			if (!StartDate.HasValue || !EndDate.HasValue)
			{
				OnGet();
			}
			else
			{
				Booking = _bookingObject.GetDataBookingByRangeDate(StartDate, EndDate);
				isSearch = true;
			}
		}
	}
}
