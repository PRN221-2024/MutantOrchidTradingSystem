using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages.CustomerUI
{
	public class BookingListModel : PageModel
	{
		public IList<BookingViewModel> Booking { get; set; }
		public string CustomerName { get; set; }
		private readonly BookingObject _bookingObject;
		public BookingListModel(BookingObject _bookingObject)
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

			if (role != "customer")
			{
				return RedirectToPage("/Login");
			}
			CustomerName = HttpContext.Session.GetString("accountName");
			var userID = HttpContext.Session.GetInt32("CustomerId");
			Booking = _bookingObject.GetDataBookingUser(userID.HasValue ? userID.Value : 0);
			return Page();
		}
	}
}
