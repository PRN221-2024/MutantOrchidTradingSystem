using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject;
using DataAccess.Models;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageCustomer
{
	public class CustomerEdit : PageModel
	{
		private readonly CustomerObject _customerObject;

		[BindProperty]
		public Customer customer { get; set; }

		[BindProperty(SupportsGet = true)]
		public int Id { get; set; }

		public CustomerEdit(CustomerObject customerObject)
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

			if (role != "admin")
			{
				return RedirectToPage("/Login");
			}
			else
			{
                if (Id == 0)
                {
                    return RedirectToPage("./ListCustomer");
                }

                customer = _customerObject.GetById(Id);

                if (customer == null)
                {
                    return RedirectToPage("/NotFound");
                }
                return Page();
            }        
		}

		public IActionResult OnPost()
		{
			var updatedCustomer = _customerObject.GetById(Id);

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
