using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapstoneWine.Data;
using CapstoneWine.Models;
using Microsoft.AspNetCore.Authorization;
using CapstoneWine.Infrastructure;
using CapstoneWine.Models.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using CapstoneWine.Areas.Services;

namespace CapstoneWine.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubscriptionsController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IEmailSender _emailSender;

        public SubscriptionsController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
			_emailSender = emailSender;
		}

        // GET: Subscriptions
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var subList = _context.Subscriptions.ToList();
            var subs = from sub in _context.Subscriptions
                        select sub;
            if (!String.IsNullOrEmpty(searchString))
            {
                subs = subs.Where(w => w.SubName.Contains(searchString));

				return View(subs);
            }

			if (String.IsNullOrEmpty(searchString))
			{
				ViewData["CurrentFilter"] = "";
			}

			
            return View(subList);
            //return _context.Subscriptions != null ? 
            //            View(await _context.Subscriptions.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Subscriptions'  is null.");
        }

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subscriptions == null)
            {
                return NotFound();
            }

            var subscriptions = await _context.Subscriptions
                .FirstOrDefaultAsync(m => m.SubID == id);
            if (subscriptions == null)
            {
                return NotFound();
            }

            return View(subscriptions);
        }

        // GET: Subscriptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubID,SubName,Type,Frequency,NumOfBottles,RewardPoints")] SubscriptionsModel subscriptions)
        {
            if (ModelState.IsValid)
            {
                int newSubID = 1; // default value for the new SubID

                // Check whether there are any records in the Subscriptions table
                if (_context.Subscriptions.Any())
                {
                    // Generate a new SubID
                    newSubID = _context.Subscriptions.Max(s => s.SubID) + 1;
                }

                // Set the new SubID value
                subscriptions.SubID = newSubID;

                // Add the subscriptions object to the context
                _context.Add(subscriptions);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Redirect to the Index page
                return RedirectToAction(nameof(Index));
            }

            // If the ModelState is not valid, return the current view with the subscriptions object
            return View(subscriptions);
        }







        // GET: Subscriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subscriptions == null)
            {
                return NotFound();
            }

            var subscriptions = await _context.Subscriptions.FindAsync(id);
            if (subscriptions == null)
            {
                return NotFound();
            }
            return View(subscriptions);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubID,SubName,Type,Frequency,NumOfBottles,RewardPoints")] SubscriptionsModel subscriptions)
        {
            if (id != subscriptions.SubID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscriptions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionsExists(subscriptions.SubID))
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
            return View(subscriptions);
        }

        // GET: Subscriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subscriptions == null)
            {
                return NotFound();
            }

            var subscriptions = await _context.Subscriptions
                .FirstOrDefaultAsync(m => m.SubID == id);
            if (subscriptions == null)
            {
                return NotFound();
            }

            return View(subscriptions);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subscriptions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Subscriptions'  is null.");
            }
            var subscriptions = await _context.Subscriptions.FindAsync(id);
            if (subscriptions != null)
            {
                _context.Subscriptions.Remove(subscriptions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
		public IActionResult OrderComplete()
		{
			List<SubItem> cart = HttpContext.Session.GetJson<List<SubItem>>("Sub") ?? new List<SubItem>();

			SubViewModel cartVM = new()
			{
				SubItems = cart,

			};

			return View(cartVM);
		}//View for OrderComplete

        string Email { get; set; }
		public async Task<IActionResult> Receipt(int id)
		{
			WinesModel wines = await _context.Wines.FindAsync(id);

			List<SubItem> cart = HttpContext.Session.GetJson<List<SubItem>>("Sub") ?? new List<SubItem>();

			SubViewModel cartVM = new()
			{
				SubItems = cart,
				GrandTotal = cart.Sum(x => x.Total + x.Shipping),
				NumOfItems = cart.Count.ToString()
			};

			if (cartVM == null)
			{
				await _emailSender.SendEmailAsync(Email = "xaviar.rehu@techtorium.ac.nz", "Order", htmlMessage: "No item selected");
			}
			else
			{
				await _emailSender.SendEmailAsync(Email = "xaviar.rehu@techtorium.ac.nz", "Order Complete", htmlMessage: "For " + cartVM.NumOfItems.ToString() + " Subscriptions, You have been charged: " + cartVM.GrandTotal.ToString("C2"));
			}

			TempData["Success"] = "The product has been added!";

			return Redirect(Request.Headers["Referer"].ToString());
		}

		private bool SubscriptionsExists(int id)
        {
          return (_context.Subscriptions?.Any(e => e.SubID == id)).GetValueOrDefault();
        }
    }
}
