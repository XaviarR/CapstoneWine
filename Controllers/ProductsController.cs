using CapstoneWine.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace CapstoneWine.Controllers
{
    public class ProductsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductsController(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			// Return an error message if the Wine entity set is null
			if (_context.Wines == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Wines' is null.");
			}
			// Otherwise, return the Wines entity set as a view
			return View(await _context.Wines.ToListAsync());
		}
	}
}
