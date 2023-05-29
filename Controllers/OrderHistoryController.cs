using CapstoneWine.Data;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneWine.Controllers
{
    public class OrderHistoryController : Controller
    {
		private readonly ApplicationDbContext _context;

		public OrderHistoryController(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Test()
		{
			// Return an error message if the Wine entity set is null
			if (_context.Subscriptions == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Wines' is null.");
			}
			// Otherwise, return the Wines entity set as a view
			return View();
		}//View for Subscription
	}
}
