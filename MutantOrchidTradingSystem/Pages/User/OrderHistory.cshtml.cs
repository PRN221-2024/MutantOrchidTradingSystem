using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public List<Order> orders { get; set; }
        public OrderHistoryModel(IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
        }
        public IActionResult OnGet()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("username");
            if (username == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                orders = _orderRepository.GetAllByAccountID((int)_httpContextAccessor.HttpContext.Session.GetInt32("Id"));
                return Page();
            }
        }
    }
}
