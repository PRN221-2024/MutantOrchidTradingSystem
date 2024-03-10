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


    public List<SelectListItem> Options { get; set; }

        [BindProperty]
    public Order NewOrder { get; set; }

    public CreateModel(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public void OnGet()
    {
            Options = _orderRepository.GetAccounts()
                    .Select(account => new SelectListItem
                    {
                        Value = account.Id.ToString(),
                        Text = account.FullName
                    })
                    .ToList();
        }

    public IActionResult OnPost()
    {
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
