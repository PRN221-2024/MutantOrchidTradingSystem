using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class ProfileModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileModel(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public Account account { get; set; }
        public IActionResult OnGet()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("username");
            if (username == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                int id = (int)_httpContextAccessor.HttpContext.Session.GetInt32("Id");
                account = _accountRepository.GetById(id);
                return Page();
            }


        }
        public IActionResult OnPost()
        {
            account.Status = true;
            _accountRepository.Update(account);
            _httpContextAccessor.HttpContext.Session.SetString("username", account.FullName);   
            return RedirectToPage("/User/Profile");
        }
    }
}
