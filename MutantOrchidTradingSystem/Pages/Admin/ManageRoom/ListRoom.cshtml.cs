using BusinessObject;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhanSyTrongRazorPage.Pages.Admin.ManageRoom
{
    public class ListRoomModel : PageModel
    {
		public IList<RoomInformation> Room { get; set; }
		[BindProperty] public string? Keyword { get; set; }
		public bool isSearch = false;

		private readonly RoomObject _roomObject;
		public ListRoomModel(RoomObject _roomObject)
		{
			this._roomObject = _roomObject;
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
			Room = _roomObject.GetAllRooms();
			return Page();
		}

		public async Task OnPost()
		{
			if (Keyword == null)
			{
				OnGet();
			}
			else
			{
				Room = _roomObject.Search(Keyword);
				isSearch = true;
			}
		}
	}
}
