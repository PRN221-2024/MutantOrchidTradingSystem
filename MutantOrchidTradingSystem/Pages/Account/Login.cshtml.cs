using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MutantOrchidTradingSysRazorPage.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly AccountObject _accountObject;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountRepository _accountRepository;
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string password { get; set; }


        public LoginModel(AccountObject accountObject, IHttpContextAccessor httpContextAccessor, IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountObject = accountObject;
            _httpContextAccessor = httpContextAccessor;
            _accountRepository = accountRepository;
            _configuration = configuration;
        }
        

        public IActionResult OnPost()
        {
            var accounts = _configuration.GetSection("AdminInfo");
            var account = _accountRepository.Login(username, password);
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (username.Equals(accounts["email"]) && password.Equals(accounts["password"]))
                {
                    _httpContextAccessor.HttpContext.Session.SetString("Role", "Admin");

                    return Redirect("/Index");
                }
                if (account != null) // Assumption: AccountObject.Login() returns an account object or null
                {
                    // Code for redirecting based on user role if needed
                    // Example:
                    // foreach (var roleAccount in account.RoleAccounts)
                    // {
                    //     if (roleAccount.RoleId == 1)
                    //     {
                    //         return Redirect("/Admin/Index");
                    //     }
                    //     else if (roleAccount.RoleId == 2)
                    //     {
                    //         _httpContextAccessor.HttpContext.Session.SetString("username", account.Username);
                    //         _httpContextAccessor.HttpContext.Session.SetInt32("Id", account.Id);
                    //         return Redirect("/");
                    //     }
                    // }

                    // If no redirection needed, redirect to homepage
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password. Please try again.");
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password. Please try again.");
                return RedirectToPage("/Index");
            }
        }
    }
}
