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

namespace CapstoneWine.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string searchString)
        {
			ViewData["CurrentFilter"] = searchString;

			var orders = from wine in _context.Orders
						select wine;
			if (!String.IsNullOrEmpty(searchString))
			{
				orders = orders.Where(w => w.UserID.Contains(searchString));
				return View(orders);
			}

			if (String.IsNullOrEmpty(searchString))
			{
				ViewData["CurrentFilter"] = "";
			}

			var applicationDbContext = _context.Orders.Include(o => o.subscription).Include(o => o.wine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.subscription)
                .Include(o => o.wine)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["SubID"] = new SelectList(_context.Subscriptions, "SubID", "SubID");
            ViewData["WineID"] = new SelectList(_context.Wines, "WineID", "WineID");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderDate,CustomerId,WineID,SubID,Quantity,DeliveryAdd,DeliveryCharge,TotalCost,OrderStatus,RewardPoints")] OrdersModel order)
        {
            if (ModelState.IsValid)
            {
                int newOrderId = 1;
                if (_context.Orders.Any())
                {
                    newOrderId = _context.Orders.Max(o => o.OrderID) + 1;
                }
                order.OrderID = newOrderId;

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SubID"] = new SelectList(_context.Subscriptions, "SubID", "SubID", order.SubID);
            ViewData["WineID"] = new SelectList(_context.Wines, "WineID", "WineID", order.WineID);
            return View(order);
        }



        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["SubID"] = new SelectList(_context.Subscriptions, "SubID", "SubID", order.SubID);
            ViewData["WineID"] = new SelectList(_context.Wines, "WineID", "WineID", order.WineID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,OrderDate,CustomerId,WineID,SubID,Quantity,DeliveryAdd,DeliveryCharge,TotalCost,OrderStatus,RewardPoints")] OrdersModel order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            ViewData["SubID"] = new SelectList(_context.Subscriptions, "SubID", "SubID", order.SubID);
            ViewData["WineID"] = new SelectList(_context.Wines, "WineID", "WineID", order.WineID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.subscription)
                .Include(o => o.wine)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}
