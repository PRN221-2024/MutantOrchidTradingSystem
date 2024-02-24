using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly AuthenticationObject _authenObject;

        [BindProperty]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        public LoginModel(AuthenticationObject authenObject)
        {
            _authenObject = authenObject;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _authenObject.isAdminLogin(Email, Password);

            if (user != null)
            {
                HttpContext.Session.SetString("account", "admin");
                HttpContext.Session.SetString("accountName", "ADMIN");
                return Redirect("/Admin/ManageCustomer/ListCustomer");
            }

            var customer = _authenObject.isCustomerLogin(Email, Password);

            if (customer != null)
            {
                HttpContext.Session.SetString("account", "customer");
                HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);
                HttpContext.Session.SetString("accountName", customer.CustomerFullName);
                return Redirect("/CustomerUI/BookingList");
            }

            ViewData["Message"] = "Username or Password is not correct!";
            ViewData["Email"] = Email;
            return Page();
        }
    }
}
