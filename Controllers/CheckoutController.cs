using CapstoneWine.Infrastructure;
using CapstoneWine.Models.ViewModels;
using CapstoneWine.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneWine.Controllers
{
	public class CheckoutController : Controller
	{
		public IActionResult Index()
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Total + x.Shipping)
			};

			return View(cartVM);
		}
	}
}
