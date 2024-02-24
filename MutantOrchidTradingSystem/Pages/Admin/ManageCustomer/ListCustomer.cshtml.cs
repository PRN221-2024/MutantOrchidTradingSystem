using BusinessObject;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageCustomer
{
    public class ListCustomerModel : PageModel
    {
        private readonly CustomerObject _customerObject;
        public ListCustomerModel(CustomerObject _customerObject)
        {
            this._customerObject = _customerObject;
        }

        public IList<Customer> Customer { get; set; }
        [BindProperty] public string? Keyword { get; set; }
        public bool isSearch = false;

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
            Customer = _customerObject.GetAll();
            return Page();
        }

        public async Task OnPost()
        {
            if (Keyword == null)
            {
                OnGet();
            }
            else
            {
                Customer = _customerObject.GetByNameContains(Keyword);
                isSearch = true;
            }
        }
    }
}
