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

    public SelectList AccountIdList { get; set; }


    [BindProperty]
    public Order NewOrder { get; set; }
    public CreateModel(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        try
        {
                NewOrder.AccountId = int.Parse(Request.Form["NewOrder.AccountId"]);
                var order = new Order()
                {
                    Created = DateTime.Now,
                    Name = "Order",
                    Status = true,
                    AccountId = NewOrder.AccountId,

                };
                _orderRepository.Add(NewOrder);
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
