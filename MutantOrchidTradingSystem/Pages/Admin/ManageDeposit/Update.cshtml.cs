using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageDeposit
{
    public class UpdateModel : PageModel
    {
        private readonly IDepositRequestRepository _depositRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHubContext<SignalServer> _bidHub;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateModel(IDepositRequestRepository depositRepository, IAccountRepository accountRepository, IHubContext<SignalServer> bidHub, IHttpContextAccessor httpContextAccessor)
        {
            _depositRepository = depositRepository;
            _accountRepository = accountRepository;
            _bidHub = bidHub;
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAcceptAsync(int depositId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            var depositRequest = _depositRepository.GetById(depositId);
            if (depositRequest != null)
            {

                depositRequest.Status = "Accepted";
                var deposit = _depositRepository.UpdateDepositRequest(depositRequest);

                var account = _accountRepository.GetById(depositRequest.AccountId);
                if (account != null)
                {

                    account.Balance += depositRequest.Amount;
                    _accountRepository.Update(account);
                    await _bidHub.Clients.All.SendAsync("UpdateProfile");
                    return RedirectToPage("/Admin/ManageDeposit/ManageDeposit");
                }

            }
            return Page();

        }
        public IActionResult OnPostReject(int depositId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")) && !_httpContextAccessor.HttpContext.Session.GetString("Role").Equals("Admin"))
            {
                return Redirect("/Login");
            }
            var depositRequest = _depositRepository.GetById(depositId);
            if (depositRequest != null)
            {

                depositRequest.Status = "Rejected";
                var deposit = _depositRepository.UpdateDepositRequest(depositRequest);
                return RedirectToPage("/Admin/ManageDeposit/ManageDeposit");
            }
            return Page();
        }
    }
}
