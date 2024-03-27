using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageOrder
{
    public class ManageOrderModel : PageModel
    {
        private readonly OrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ManageOrderModel(OrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Order> listOrders { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            if (_httpContextAccessor.HttpContext.Session.GetString("Role") != ("Admin")){
                return Redirect("/Login");
            }

            listOrders = _orderRepository.GetAll();
            return Page();
        }
    }
}
