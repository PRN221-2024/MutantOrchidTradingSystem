using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageCustomer
{
    public class CreateModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleAccountRepository _roleAccountRepository;
        public CreateModel(IAccountRepository accountRepository, IRoleAccountRepository roleAccountRepository)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
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
                return RedirectToPage("./ManageCustomer");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPost - CreateModel: {ex.Message}");
                return Page();
            }
        }
    }
}
