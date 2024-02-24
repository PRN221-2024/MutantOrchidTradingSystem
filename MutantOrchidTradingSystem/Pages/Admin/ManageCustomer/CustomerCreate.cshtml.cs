using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Models;
using BusinessObject;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageCustomer
{
    public class CustomerCreate : PageModel
    {
        private readonly CustomerObject _customerObject;

        public CustomerCreate(CustomerObject customerObject)
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
            return Page();
        }

        [BindProperty]
        public DataAccess.Models.Customer customer { get; set; }

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
            _customerObject.Create(customer);
            return Redirect("./ListCustomer");
        }
    }
}
