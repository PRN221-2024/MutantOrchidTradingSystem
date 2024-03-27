using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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

        [BindProperty]
        public string ConfirmPassword { get; set; }
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
            if (Account.Password != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu và xác nhận mật khẩu không khớp.");
                return Page();
            }

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
            return RedirectToPage("/Login");
        }
    }
}
