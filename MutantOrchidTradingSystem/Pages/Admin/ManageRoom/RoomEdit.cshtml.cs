using BusinessObject;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageRoom
{
    public class RoomEditModel : PageModel
    {
        private readonly RoomObject _roomObject;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public RoomEditModel(RoomObject roomObject)
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
            else
            {
                if (Id == 0)
                {
                    return RedirectToPage("./ListRoom");
                }

                room = _roomObject.GetRoomById(Id);

                if (room == null)
                {
                    return RedirectToPage("/NotFound");
                }
                return Page();
            }
        }

        [BindProperty]
        public RoomInformation room { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            var updatedRoom = _roomObject.GetRoomById(Id);

            if (updatedRoom == null)
            {
                return RedirectToPage("/NotFound");
            }

            // Bind properties directly from the model
            TryUpdateModelAsync(updatedRoom, "Room");

            _roomObject.UpdateRoom(updatedRoom);

            return RedirectToPage();
        }
    }
}
