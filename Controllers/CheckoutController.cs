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
		[HttpPost]
		public async Task<IActionResult> IndexAsync(string ShippingEmail, string ShippingName)
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Total + x.Shipping),
				NumOfItems = cart.Count.ToString()
			};
			
			Email = ShippingEmail;
			await _emailSender.SendEmailAsync(Email, $"Order Complete {ShippingName}", htmlMessage: $"For {cartVM.NumOfItems} Items, You have been charged: {cartVM.GrandTotal.ToString("C2")}");
			return Redirect(Request.Headers["Referer"].ToString());
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

		public async Task<IActionResult> Receipt(int id, string ShippingEmail)
		{
			WinesModel wines = await _context.Wines.FindAsync(id);

			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Total + x.Shipping),
				NumOfItems = cart.Count.ToString()
			};

			CartItem Items = new()
			{

				ProductName = cart.First().ProductName,
				Price = cart.First().Price,
				Quantity = cart.First().Quantity,
			};

			if (cartVM == null)
			{
				await _emailSender.SendEmailAsync(Email, "Order", htmlMessage: "No item selected");
			}
			else
			{
				await _emailSender.SendEmailAsync(Email, "Order Complete", htmlMessage: "For " + cartVM.NumOfItems.ToString() + " Items, You have been charged: " + cartVM.GrandTotal.ToString("C2"));
				//await _emailSender.SendEmailAsync(Email = "xaviar.rehu@techtorium.ac.nz", "Order", 
				//	htmlMessage: 
				//	Items.ProductName + 
				//	" " + Items.Price.ToString("C2") + 
				//	" " + Items.Quantity + 
				//	" " + cartVM.GrandTotal.ToString("C2"));
			}

			TempData["Success"] = "The product has been added!";

			return Redirect(Request.Headers["Referer"].ToString());
		}
	}
}
