using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageDeposit
{
    public class ManageDepositModel : PageModel
    {
        private readonly IDepositRequestRepository _depositRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        public ManageDepositModel(IDepositRequestRepository depositRepository, IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _depositRepository = depositRepository;
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<DepositRequest> DepositRequests { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            if (_httpContextAccessor.HttpContext.Session.GetString("Role") != ("Admin"))
            {
                return Redirect("/Login");
            }
            DepositRequests = _depositRepository.GetAll();
            return Page();
        }

       
    }
}
