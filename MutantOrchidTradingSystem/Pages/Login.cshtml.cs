using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
           
            var account = _accountRepository.Login(username, password);
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
               
                if (account != null) // Assumption: AccountObject.Login() returns an account object or null
                {
                    // Code for redirecting based on user role if needed
                   foreach(var role in account.RoleAccounts) {                        
                        if(role.RoleId == 1)
                        {
                            _httpContextAccessor.HttpContext.Session.SetString("username", account.FullName);
                            _httpContextAccessor.HttpContext.Session.SetInt32("Id", account.Id);
                            _httpContextAccessor.HttpContext.Session.SetString("Role", "Admin");
                            return Redirect("/Admin/ManageCustomer/ManageCustomer");
                        }
                        else if(role.RoleId == 2)
                        {
                            _httpContextAccessor.HttpContext.Session.SetString("username", account.FullName);
                            _httpContextAccessor.HttpContext.Session.SetInt32("Id", account.Id);
                            _httpContextAccessor.HttpContext.Session.SetString("Role", "Customer");
                            return Redirect("/");
                        }else if(role.RoleId == 3)
                        {
                            _httpContextAccessor.HttpContext.Session.SetString("username", account.FullName);
                            _httpContextAccessor.HttpContext.Session.SetInt32("Id", account.Id);
                            _httpContextAccessor.HttpContext.Session.SetString("Role", "Staff");
                            return Redirect("/Staff/Auction");
                        }
                    }
                   return Redirect("/");
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password. Please try again.");
                    return Page();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password. Please try again.");
                return Page();
            }
        }
    }
}
