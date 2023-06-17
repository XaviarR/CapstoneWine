using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapstoneWine.Data;
using CapstoneWine.Models;

namespace CapstoneWine.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var subscriptions = await _context.Subscriptions.ToListAsync();
            var wines = await _context.Wines.ToListAsync();
            var orders = await _context.Orders.Include(o => o.wine).ToListAsync();
            var customers = await _context.CustomerModel.ToListAsync();

            var model = new Tuple<List<SubscriptionsModel>, List<WinesModel>, List<OrdersModel>, List<CustomerModel>>(subscriptions, wines, orders, customers);


			// No. Of Wines
			int wineCount = wines.Select(w => w.WineID).Count();
			ViewData["WineCount"] = wineCount;

			// No. of Users
            int userCount = customers.Count();
            ViewData["UserCount"] = userCount;

            // Admin Users 
            int adminCount = _context.UserRoles.Where(a => a.RoleId == "ada167e6-5ad7-440e-87ec-86d7ab9d52a7").Count();
            ViewData["AdminCount"] = adminCount;

            // Subscribers
            

            // Earnings (can probably make it up)


            // Pending Orders
            int pendingOrders = orders.Where(a => a.OrderStatus == "Pending").Count();
            ViewData["PendingOrders"] = pendingOrders;

            // Sales



			return View(model);
        }

        public async Task<IActionResult> Orders()
        {
            var applicationDbContext = _context.Orders.Include(o => o.wine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var ordersModel = await _context.Orders
                .Include(o => o.wine)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (ordersModel == null)
            {
                return NotFound();
            }

            return View(ordersModel);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["SubID"] = new SelectList(_context.Subscriptions, "SubID", "SubID");
            ViewData["WineID"] = new SelectList(_context.Wines, "WineID", "WineID");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderDate,CustomerId,WineID,SubID,Quantity,DeliveryAdd,DeliveryCharge,TotalCost,OrderStatus,RewardPoints")] OrdersModel ordersModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordersModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WineID"] = new SelectList(_context.Wines, "WineID", "WineID", ordersModel.WineID);
            return View(ordersModel);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var ordersModel = await _context.Orders.FindAsync(id);
            if (ordersModel == null)
            {
                return NotFound();
            }
            ViewData["WineID"] = new SelectList(_context.Wines, "WineID", "WineID", ordersModel.WineID);
            return View(ordersModel);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,OrderDate,CustomerId,WineID,SubID,Quantity,DeliveryAdd,DeliveryCharge,TotalCost,OrderStatus,RewardPoints")] OrdersModel ordersModel)
        {
            if (id != ordersModel.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordersModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersModelExists(ordersModel.OrderID))
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
            ViewData["WineID"] = new SelectList(_context.Wines, "WineID", "WineID", ordersModel.WineID);
            return View(ordersModel);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var ordersModel = await _context.Orders
                .Include(o => o.wine)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (ordersModel == null)
            {
                return NotFound();
            }

            return View(ordersModel);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            var ordersModel = await _context.Orders.FindAsync(id);
            if (ordersModel != null)
            {
                _context.Orders.Remove(ordersModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersModelExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}
