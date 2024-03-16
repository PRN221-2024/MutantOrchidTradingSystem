using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages.User
{
    public class PaymentBidModel : PageModel
    {
        private readonly IBidRepository _bidRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public PaymentBidModel(IBidRepository bidRepository, IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _bidRepository = bidRepository;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public Bid bid { get; set; }
        public decimal total { get; set; }
        public IActionResult OnGet(int id)
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("username");
            if (username == null)
            {
                return RedirectToPage("/Login");
            }
            bid = _bidRepository.GetById(id);
            total = bid.Amount + 20 + 5;
            return Page();
        }

        public IActionResult OnGetCheckOut(int id)
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("username");
            if (username == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                var Order = new Order
                {
                    Created = DateTime.Now,
                    Name = "Auction Winner",
                    Status = true,
                    AccountId = _httpContextAccessor.HttpContext.Session.GetInt32("Id")

                };

                var order = _orderRepository.Add(Order);
                var bid = _bidRepository.GetById(id);
                var total = bid.Amount + 20 + 5;
                var orderDetail = new OrderDetail
                        {
                            OrderId = order.Id,
                            ProductId = bid.Auction.ProductId,
                            Quantity = 1,
                            Price = total
                        };
                        _orderDetailRepository.AddOrderDetail(orderDetail);
                
                return RedirectToPage("/ThankYou");
            }
        }
    }
}
