using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult OnGet()
        {
            _httpContextAccessor.HttpContext.Session.Clear();


            return Redirect("/");
        }
    }
}
