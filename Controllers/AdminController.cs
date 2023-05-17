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
            var applicationDbContext = _context.Orders.Include(o => o.subscription).Include(o => o.wine);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Orders()
        {
            var applicationDbContext = _context.Orders.Include(o => o.subscription).Include(o => o.wine);
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
                .Include(o => o.subscription)
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
        public async Task<IActionResult> Create([Bind("OrderID,OrderDate,UserID,WineID,SubID,Quantity,DeliveryAdd,DeliveryCharge,TotalCost,OrderStatus,RewardPoints")] OrdersModel ordersModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordersModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubID"] = new SelectList(_context.Subscriptions, "SubID", "SubID", ordersModel.SubID);
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
            ViewData["SubID"] = new SelectList(_context.Subscriptions, "SubID", "SubID", ordersModel.SubID);
            ViewData["WineID"] = new SelectList(_context.Wines, "WineID", "WineID", ordersModel.WineID);
            return View(ordersModel);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,OrderDate,UserID,WineID,SubID,Quantity,DeliveryAdd,DeliveryCharge,TotalCost,OrderStatus,RewardPoints")] OrdersModel ordersModel)
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
            ViewData["SubID"] = new SelectList(_context.Subscriptions, "SubID", "SubID", ordersModel.SubID);
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
                .Include(o => o.subscription)
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
