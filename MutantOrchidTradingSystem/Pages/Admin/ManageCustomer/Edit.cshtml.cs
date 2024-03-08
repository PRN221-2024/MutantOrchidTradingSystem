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
            Console.WriteLine("OnPost method reached");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is not valid");
                return Page();
            }

            _accountRepository.Update(UpdatedAccount);
            Console.WriteLine("Product updated successfully");
            return RedirectToPage("ManageProduct");
        }
    }
}
