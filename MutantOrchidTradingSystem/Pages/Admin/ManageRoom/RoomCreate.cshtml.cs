using BusinessObject;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageRoom
{
    public class RoomCreateModel : PageModel
    {
        private readonly RoomObject _roomObject;

        public RoomCreateModel(RoomObject roomObject)
        {
            _roomObject = roomObject;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("account") is null)
            {
                return RedirectToPage("/Login");
            }

            var role = HttpContext.Session.GetString("account");

            if (role != "admin")
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }

        [BindProperty]
        public RoomInformation room { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Additional validation logic

            _roomObject.Create(room);
            return Redirect("./ListRoom");
        }
    }
}
