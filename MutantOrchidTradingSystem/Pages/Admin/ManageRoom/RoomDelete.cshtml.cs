using BusinessObject;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageRoom
{
    public class RoomDeleteModel : PageModel
    {
        private readonly RoomObject _roomObject;

        public RoomDeleteModel(RoomObject _roomObject)
        {
            this._roomObject = _roomObject;
        }

        [BindProperty]
        public RoomInformation room { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            room = _roomObject.GetRoomById(id);

            if (room == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            room = _roomObject.GetRoomById(id);

            if (room != null)
            {
                _roomObject.DeleteRoom(room);
            }

            return RedirectToPage("./ListRoom");
        }
    }
}
