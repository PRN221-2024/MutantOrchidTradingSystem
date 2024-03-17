using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageCustomer
{
    public class EditModel : PageModel
    {
        private readonly AccountRepository _accountRepository;

        [BindProperty]
        public Account UpdatedAccount { get; set; }
        public EditModel(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult OnGet(int accountId)
        {
            UpdatedAccount = _accountRepository.GetById(accountId);

            if (UpdatedAccount != null)
            {
                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var originalAccount = _accountRepository.GetById(UpdatedAccount.Id);

            if (UpdatedAccount.Username != originalAccount.Username)
            {
                List<string> existingUsernames = _accountRepository.GetAllUsername();
                if (existingUsernames.Contains(UpdatedAccount.Username))
                {
                    TempData["UsernameError"] = "The username is already in use. Please choose a different username.";
                    return Page();
                }
            }

            _accountRepository.Update(UpdatedAccount);

            Console.WriteLine("Account updated successfully");
            return RedirectToPage("ManageCustomer");
        }
    }

}

