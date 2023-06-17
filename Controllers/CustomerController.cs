using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneWine.Data;
using CapstoneWine.Models.ViewModels;
using CapstoneWine.Services;
using CapstoneWine.Models;
using Microsoft.AspNetCore.Identity;

namespace CapstoneWine.Controllers
{
	public class CustomerController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IAccountService _accountService;
		private readonly UserManager<IdentityUser> _userManager;

		public CustomerController(
			ApplicationDbContext context,
			IAccountService accountService,
			UserManager<IdentityUser> userManager
			)
		{
			_context = context;
			_accountService = accountService;
			_userManager = userManager;
		}

		// GET: CustomerModels
		public async Task<IActionResult> Index(string searchString)
		{
			ViewData["CurrentFilter"] = searchString;

			IEnumerable<CustomerModel> customers;

			if (!String.IsNullOrEmpty(searchString))
			{
				customers = await _context.CustomerModel.Where(c => c.FirstName.Contains(searchString)).ToListAsync();
			}
			else
			{
				customers = await _context.CustomerModel.ToListAsync();
				ViewData["CurrentFilter"] = "";
			}


			var customerViewModels = new List<CustomerViewModel>();

			foreach (var customer in customers)
			{
				var user = _context.Users.Where(u => u.Id == customer.IdentityKey).First();
				var customerViewModel = new CustomerViewModel();
				customerViewModel.Customer = customer;
				customerViewModel.Email = user.Email;
				customerViewModels.Add(customerViewModel);
			}

			return View(customerViewModels);

		}

		// GET: CustomerModels/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.CustomerModel == null)
			{
				return NotFound();
			}

			var customerModel = await _context.CustomerModel
				.FirstOrDefaultAsync(m => m.ID == id);
			if (customerModel == null)
			{
				return NotFound();
			}

			var customerViewModel = new CustomerViewModel()
			{
				Customer = customerModel,
				Email = _context.Users.Where(u => u.Id == customerModel.IdentityKey).First().Email
			};

			return View(customerViewModel);
		}

		// GET: CustomerModels/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: CustomerModels/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,StreetAddress,Suburb,City,PostCode")] CustomerViewModel customerViewModel)
		public async Task<IActionResult> Create(CustomerViewModel customerViewModel)
		{
			if (ModelState.IsValid)
			{
				//Create user first
				var user = _accountService.CreateAccountAsync(customerViewModel.Email, "Test/123");

				customerViewModel.Customer.IdentityKey = user.Result.Id;

				Console.WriteLine(customerViewModel.Customer.IdentityKey);

				await _accountService.CreateCustomerAsync(customerViewModel.Customer);


				return RedirectToAction(nameof(Index));
			}
			return View(customerViewModel);
		}


		// GET: CustomerModels/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.CustomerModel == null)
			{
				return NotFound();
			}

			var customerModel = await _context.CustomerModel.FindAsync(id);
			if (customerModel == null)
			{
				return NotFound();
			}

			var customerViewModel = new CustomerViewModel()
			{
				Customer = customerModel,
				Email = _context.Users.Where(u => u.Id == customerModel.IdentityKey).First().Email
			};

			return View(customerViewModel);
		}

		// POST: CustomerModels/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,StreetAddress,Suburb,City,PostCode,")] CustomerModel customerModel)
		public async Task<IActionResult> Edit(int id, CustomerViewModel customerViewModel)
		{
			if (id != customerViewModel.Customer.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(customerViewModel);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CustomerModelExists(customerViewModel.Customer.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(customerViewModel);
		}

		// GET: CustomerModels/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.CustomerModel == null)
			{
				return NotFound();
			}

			var customerModel = await _context.CustomerModel
				.FirstOrDefaultAsync(m => m.ID == id);
			if (customerModel == null)
			{
				return NotFound();
			}

			var customerViewModel = new CustomerViewModel()
			{
				Customer = customerModel,
				Email = _context.Users.Where(u => u.Id == customerModel.IdentityKey).First().Email
			};

			return View(customerViewModel);
		}

		// POST: CustomerModels/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.CustomerModel == null)
			{
				return Problem("Entity set 'ApplicationDbContext.CustomerModel'  is null.");
			}
			var customerModel = await _context.CustomerModel.FindAsync(id);
			if (customerModel != null)
			{
				_context.CustomerModel.Remove(customerModel);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Profile()
		{
			var idKey = _userManager.GetUserId(HttpContext.User);

			var customer = await _context.CustomerModel.FirstOrDefaultAsync(a => a.IdentityKey == idKey);

			if (customer == null)
			{
				return NotFound();
			}

			var customerId = customer.ID;

			var orders = _context.Orders.Where(c => c.CustomerId == customerId).ToArray();

			var wineIds = orders.Select(o => o.WineID).ToArray();
			var wines = await _context.Wines.Where(w => wineIds.Contains(w.WineID)).ToListAsync();


			var customerSub = await _context.CustomerSub.FirstOrDefaultAsync(b => b.CustomerID == customerId);
			var subId = customerSub.SubID;
			var subscription = await _context.Subscriptions.FirstOrDefaultAsync(e => e.SubID == subId);


			var customerViewModel = new CustomerViewModel()
			{
				Customer = customer,
				Email = _context.Users.Where(u => u.Id == customer.IdentityKey).First().Email,
				Subscription = subscription,
				Orders = orders,
				Wines = wines,
			};

			return View(customerViewModel);
		}

		private bool CustomerModelExists(int id)
		{
			return (_context.CustomerModel?.Any(e => e.ID == id)).GetValueOrDefault();
		}

	}
}
