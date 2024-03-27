using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageOrder
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public OrderDetail NewOrderDetail { get; set; }


        public List<SelectListItem> Options { get; set; }

        public OrderDetailModel(OrderDetailRepository orderDetailRepository, OrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet(int orderId)
        {
            Options = _orderDetailRepository.GetProducts().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name,
            }).ToList();

            Order order = _orderRepository.GetById(orderId);

            if (order != null)
            {
                NewOrderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                };
                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var orderDetail = new OrderDetail()
                {
                    ProductId = NewOrderDetail.ProductId,
                    Quantity = NewOrderDetail.Quantity,
                    Price = NewOrderDetail.Price,
                    OrderId = NewOrderDetail.OrderId,
                };
                _orderDetailRepository.AddOrderDetail(orderDetail);
                return RedirectToPage("ManageOrder");
            }catch(Exception ex)
            {
                Console.WriteLine($"Error OnPost in OrderDetail - ManageOrder{ex.Message}");
                throw;
            }
        }
    }
}
