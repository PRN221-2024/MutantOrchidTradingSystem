using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MutantOrchidTradingSysRazorPage.Pages.Admin.ManageOrder
{
public class CreateModel : PageModel
{
    private readonly OrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
    public List<SelectListItem> Options { get; set; }

        [BindProperty]
    public Order NewOrder { get; set; }

    public CreateModel(OrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet()
    {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return Redirect("/Login");
            }

            Options = _orderRepository.GetAccounts()
                    .Select(account => new SelectListItem
                    {
                        Value = account.Id.ToString(),
                        Text = account.FullName
                    })
                    .ToList();

            return Page();
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
                var order = new Order()
                {
                    Created = DateTime.Now,
                    Name = "Order",
                    Status = true,
                    AccountId = NewOrder.AccountId,

                };
                _orderRepository.Add(order);

                int orderId = order.Id;
                return RedirectToPage("ManageOrder");
            }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnPost - CreateModel: {ex.Message}");
            return Page();
        }
    }
    }
}
