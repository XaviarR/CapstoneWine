using CapstoneWine.Data;
using CapstoneWine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CapstoneWine.Controllers
{
	public class ShopController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ShopController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var allWine = _context.Wines.ToList();
			var categories = allWine.Select(w => w.Category).Distinct().ToArray();
			ViewData["Categories"] = categories;

			var types = allWine.Select(w => w.Type).Distinct().ToArray();
			ViewData["Types"] = types;

			return View(allWine);
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

		public IActionResult Category(string categoryFilter)
		{
			var allWine = _context.Wines.ToList();
			ViewData["CurrentFilter"] = categoryFilter;
			var categories = from category in _context.Wines select category;

			if (!String.IsNullOrEmpty(categoryFilter))
			{
				categories = categories.Where(c => c.Category.Contains(categoryFilter));

				if (!categories.Any())
				{
					return RedirectToAction("Index", "Shop");
				}

				return View(categories);
			}

			return View(allWine);
		}

		public IActionResult Type(string typeFilter)
		{
			var allWine = _context.Wines.ToList();
			ViewData["CurrentFilter"] = typeFilter;
			var types = from type in _context.Wines select type;

			if (!String.IsNullOrEmpty(typeFilter))
			{
				types = types.Where(c => c.Type.Contains(typeFilter));

				if (!types.Any())
				{
					return RedirectToAction("Index", "Shop");
				}

				return View(types);
			}

			return View(allWine);
		}

		public IActionResult Price(int lowerVal, int upperVal)
		{
			var allWine = _context.Wines.ToList();
			ViewData["LowerValue"] = lowerVal;
			ViewData["UpperValue"] = upperVal;

			//ViewData["CurrentFilter"] = priceSort;
			var prices = from price in _context.Wines select price;

			prices = prices.Where(p => p.Price >= lowerVal && p.Price <= upperVal);

			return View(prices);
		}

	}

}
