using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Order order { get; set; }
        public OrderDetailModel(IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
        }
        public IActionResult OnGet(int id)
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("username");
            if (username == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                order = _orderRepository.GetById(id);
                return Page();
            }
        }
    }
}
