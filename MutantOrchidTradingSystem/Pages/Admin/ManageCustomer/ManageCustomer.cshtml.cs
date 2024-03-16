using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageCustomer
{
    public class ManageCustomerModel : PageModel
    {
        private readonly AccountRepository _accountRepository;
        public ManageCustomerModel(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public List<Account> listAccounts { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            listAccounts = _accountRepository.GetAll();
            return Page();
        }
    }
}
