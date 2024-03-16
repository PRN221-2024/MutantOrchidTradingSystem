using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageOrder
{
    public class ManageOrderModel : PageModel
    {
        private readonly OrderRepository _orderRepository;
        public ManageOrderModel(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> listOrders { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }

            listOrders = _orderRepository.GetAll();
            return Page();
        }
    }
}
