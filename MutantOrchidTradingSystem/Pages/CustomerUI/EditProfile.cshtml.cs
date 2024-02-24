using BusinessObject;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages.CustomerUI
{
    public class EditProfileModel : PageModel
    {
		private readonly CustomerObject _customerObject;

		[BindProperty]
		public Customer customer { get; set; }

		public EditProfileModel(CustomerObject customerObject)
		{
			_customerObject = customerObject;
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
			else
			{
				var userID = HttpContext.Session.GetInt32("CustomerId");

				if (!userID.HasValue)
				{
					return RedirectToPage("./BookingList");
				}
				customer = _customerObject.GetById(userID.Value);

				if (customer == null)
				{
					return RedirectToPage("/NotFound");
				}
				return Page();
			}
		}

		public IActionResult OnPost()
		{
			var userID = HttpContext.Session.GetInt32("CustomerId");

			if (!userID.HasValue)
			{
				return RedirectToPage("./BookingList");
			}
			var updatedCustomer = _customerObject.GetById(userID.Value);

			if (updatedCustomer == null)
			{
				return RedirectToPage("/NotFound");
			}

			// Additional validation logic
			if (customer.CustomerBirthday > DateTime.Now)
			{
				ModelState.AddModelError("customerBirthday", "Birthdate cannot be in the future.");
				return Page();
			}

			if (!customer.CustomerBirthday.HasValue)
			{
				ModelState.AddModelError("customerBirthday", "Birthdate cannot null");
				return Page();
			}

			// Bind properties directly from the model
			TryUpdateModelAsync(updatedCustomer, "Customer");

			_customerObject.Update(updatedCustomer);

			return RedirectToPage();
		}
	}
}
