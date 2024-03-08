using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageCustomer
{
    public class DeleteModel : PageModel
    {
        private readonly AccountRepository _accountRepository;

        public DeleteModel(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [BindProperty]
        public Account Account { get; set; }

        public IActionResult OnGet(int accountId)
        {
            Account = _accountRepository.GetById(accountId);

            if (Account == null)
            {
                return RedirectToPage("./ManageProduct");
            }
            else
            {
                return Page();
            }
        }


        public IActionResult OnPost()
        {
            var accountId = Account.Id;
            var existingAccount = _accountRepository.GetById(accountId);

            if (existingAccount != null)
            {
                _accountRepository.Delete(accountId);
                return RedirectToPage("ManageCustomer");
            }
            else
            {
                return Page(); ;
            }
        }
    }
}
