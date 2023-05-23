using CapstoneWine.Data;
using CapstoneWine.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneWine.Controllers
{
	public class ShopController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ShopController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index(string searchString)
		{
			ViewData["CurrentFilter"] = searchString;
			var wineList = _context.Wines.ToList();
			var wines = from wine in _context.Wines select wine;


			if (!String.IsNullOrEmpty(searchString))
			{
				wines = wines.Where(w => w.WineName.Contains(searchString));

				if (wines.Count() == 0)
				{
					ViewData["ErrorMessage"] = "No matches found";
					return View(wineList);
				}

				return View(wines);
			}

			var categories = wineList.Select(w => w.Category).Distinct().ToArray();
			ViewData["Categories"] = categories;

			return View(wineList);
		}

		public IActionResult Product(string searchString)
		{
			ViewData["CurrentFilter"] = searchString;
			var wines = from wine in _context.Wines select wine;

			if (!String.IsNullOrEmpty(searchString))
			{
				wines = wines.Where(w => w.WineName.Contains(searchString));

				if (!wines.Any())
				{
					return RedirectToAction("Index", "Shop");
				}

				return View(wines);
			}

			return RedirectToAction("Index", "Shop");
		}
	}
}
