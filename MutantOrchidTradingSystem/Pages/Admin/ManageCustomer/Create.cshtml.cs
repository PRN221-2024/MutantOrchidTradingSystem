using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            Account = new Account();
            return Page();
        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                List<string> existingUsernames = _accountRepository.GetAllUsername();
                var validationResult = Account.UsernameNotIdentical(existingUsernames);
                if (validationResult != ValidationResult.Success)
                {
                    TempData["UsernameError"] = validationResult.ErrorMessage;
                    return Page();
                }
                Account.Balance = 0;
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
