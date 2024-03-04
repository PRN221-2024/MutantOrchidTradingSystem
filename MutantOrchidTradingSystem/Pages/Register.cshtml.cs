using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleAccountRepository _roleAccountRepository;
        public RegisterModel(IAccountRepository accountRepository, IRoleAccountRepository roleAccountRepository)
        {
            _accountRepository = accountRepository;
            _roleAccountRepository = roleAccountRepository;
        }
        [BindProperty]
        public Account Account { get; set; }
        public void OnGet()
        {
            Account = new Account();
        }
        public IActionResult OnPost()
        {
            Account.Status = true;
            var account = _accountRepository.Register(Account);
            RoleAccount roleAccount = new RoleAccount
            {
                AccountId = account.Id,
                RoleId = 2,
                Status = true
            };
            _roleAccountRepository.AddRoleAccount(roleAccount);
            return RedirectToPage("/Login");
        }
    }
}
