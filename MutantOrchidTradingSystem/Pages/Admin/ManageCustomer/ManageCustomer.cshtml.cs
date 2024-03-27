using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageCustomer
{
    public class ManageCustomerModel : PageModel
    {
        private readonly AccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ManageCustomerModel(AccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Account> listAccounts { get; set; }
        public IActionResult OnGet()
        {   
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Role")))
            {
                return Redirect("/Login");
            }
            if (_httpContextAccessor.HttpContext.Session.GetString("Role") != ("Admin"))
            {
                return Redirect("/Login");
            }
            listAccounts = _accountRepository.GetAll();
            var listRole = _accountRepository.RoleAccounts().Where(x => x.RoleId != 1).Select(x => x.AccountId);
            listAccounts = (from user in listAccounts
                            where listRole.Contains(user.Id)
                            select user).ToList();
            return Page();
        }
    }
}
