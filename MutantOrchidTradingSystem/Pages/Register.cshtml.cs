using System.ComponentModel.DataAnnotations;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly CustomerObject _customerObject;

        public RegisterModel(CustomerObject customerObject)
        {
            _customerObject = customerObject;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataAccess.Models.Customer customer { get; set; }
        [BindProperty]
        public string? ConfirmPass { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Additional validation logic
            if (customer.CustomerBirthday > DateTime.Now)
            {
                ModelState.AddModelError("customer.CustomerBirthday", "Birthdate cannot be in the future.");
                return Page();
            }

            if (customer.Password != ConfirmPass)
            {
                ModelState.AddModelError("ConfirmPass", "Password and Confirm Password must match.");
                ModelState.AddModelError("customer.Password", "Password and Confirm Password must match.");
                return Page();
            }

            _customerObject.Create(customer);
            return Redirect("./Login");
        }
    }
}
