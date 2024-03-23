using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class DepositRequestModel : PageModel
    {
        private readonly IDepositRequestRepository _depositRequestRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<SignalServer> _bidHub;
        public DepositRequestModel(IDepositRequestRepository depositRequestRepository, IHttpContextAccessor httpContextAccessor, IHubContext<SignalServer> hubContext)
        {
            _depositRequestRepository = depositRequestRepository;
            _httpContextAccessor = httpContextAccessor;
            _bidHub = hubContext;
        }
        public List<DepositRequest> depositRequests { get; set; }
        [BindProperty]
        public DepositRequest depositRequest { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            depositRequests = _depositRequestRepository.GetListByAccountId(_httpContextAccessor.HttpContext.Session.GetInt32("Id").Value);
            depositRequest = new DepositRequest();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            depositRequests = _depositRequestRepository.GetListByAccountId(_httpContextAccessor.HttpContext.Session.GetInt32("Id").Value);
            if (depositRequest.Amount < 50000 || depositRequest.Amount > 100000000)
            {
                ModelState.AddModelError("depositRequest.Amount", "Số tiền phải lớn hơn 0 và nhỏ hơn 100000000.");
                return Page();
            }
                depositRequest.Date = DateTime.Now;
                depositRequest.AccountId = _httpContextAccessor.HttpContext.Session.GetInt32("Id").Value;
                depositRequest.Status = "Pending";
                _depositRequestRepository.Create(depositRequest);
                await _bidHub.Clients.All.SendAsync("UpdateDepositList");
                return RedirectToPage("/User/Profile");
            
            
        }
    }
}
