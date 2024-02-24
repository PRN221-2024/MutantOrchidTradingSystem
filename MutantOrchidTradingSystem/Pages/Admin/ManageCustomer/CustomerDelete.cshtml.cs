using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject;
using DataAccess.Models;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageCustomer
{
    public class CustomerDelete : PageModel
    {
        private readonly CustomerObject _customerObject;

        public CustomerDelete(CustomerObject customerObject)
        {
            _customerObject = customerObject;
        }

        [BindProperty]
        public Customer customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            customer = _customerObject.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            customer = _customerObject.GetById(id);

            if (customer != null)
            {
                _customerObject.Delete(customer);
            }

            return RedirectToPage("./ListCustomer");
        }
    }
}
