using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class TransactionModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDeductionRequestRepository _deductionRepository;
        private readonly IAccountRepository _accountRepository;
        public TransactionModel(IHttpContextAccessor httpContextAccessor, IDeductionRequestRepository deductionRepository, IAccountRepository accountRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _deductionRepository = deductionRepository;
            _accountRepository = accountRepository;
        }
        public List<DeductionRequest> DeductionRequests { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            DeductionRequests = _deductionRepository.GetListByAccountId(_httpContextAccessor.HttpContext.Session.GetInt32("Id").Value);
            return Page();
        }
    }
}
