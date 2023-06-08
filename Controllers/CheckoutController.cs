using CapstoneWine.Infrastructure;
using CapstoneWine.Models.ViewModels;
using CapstoneWine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis;
using CapstoneWine.Data;
using CapstoneWine.Areas.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CapstoneWine.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IEmailSender _emailSender;
		public CheckoutController(ApplicationDbContext context, IEmailSender emailSender)
		{
			_context = context;
			_emailSender = emailSender;
		}

		public string Email { get; private set; }
		[HttpGet]
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

		[HttpGet]
		public IActionResult ShippingDetails()
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Total + x.Shipping)
			};

			return View(cartVM);
		}//View for ShippingDetails

		//Gets the values from the Form in the shipping details page
		[HttpPost]
		public async Task<IActionResult> ShippingDetailsAsync(string ShippingEmail, string ShippingName, string ShippingMobile, string ShippingAddress)
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Total + x.Shipping),
				NumOfItems = cart.Count.ToString()
			};

			Email = ShippingEmail;
			await _emailSender.SendEmailAsync(Email, $"Order Complete {ShippingName}", htmlMessage: $"For {cartVM.NumOfItems} Items" +
				$"<br>You will be charged: {cartVM.GrandTotal.ToString("C2")}" +
				$"<br>Mobile: {ShippingMobile}" +
				$"<br>They will be delivered to {ShippingAddress}");

			//return RedirectToAction("OrderComplete");
			return Redirect("Index");
		}
		public IActionResult OrderComplete()
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartViewModel cartVM = new()
			{
				CartItems = cart,
			};

			return View(cartVM);
		}//View for OrderComplete

	}
}
