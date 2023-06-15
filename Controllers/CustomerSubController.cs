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
    public class CustomerSubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerSubController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerSub
        public async Task<IActionResult> Index()
        {
              return _context.CustomerSub != null ? 
                          View(await _context.CustomerSub.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerSub'  is null.");
        }

        // GET: CustomerSub/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerSub == null)
            {
                return NotFound();
            }

            var customerSubModel = await _context.CustomerSub
                .FirstOrDefaultAsync(m => m.CustomerSubID == id);
            if (customerSubModel == null)
            {
                return NotFound();
            }

            return View(customerSubModel);
        }

        // GET: CustomerSub/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerSub/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerSubID,SubID,CustomerID,StartDate")] CustomerSubModel customerSubModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerSubModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerSubModel);
        }

        // GET: CustomerSub/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerSub == null)
            {
                return NotFound();
            }

            var customerSubModel = await _context.CustomerSub.FindAsync(id);
            if (customerSubModel == null)
            {
                return NotFound();
            }
            return View(customerSubModel);
        }

        // POST: CustomerSub/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("CustomerSubID,SubID,CustomerID,StartDate")] CustomerSubModel customerSubModel)
        {
            if (id != customerSubModel.CustomerSubID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerSubModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerSubModelExists(customerSubModel.CustomerSubID))
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
            return View(customerSubModel);
        }

        // GET: CustomerSub/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerSub == null)
            {
                return NotFound();
            }

            var customerSubModel = await _context.CustomerSub
                .FirstOrDefaultAsync(m => m.CustomerSubID == id);
            if (customerSubModel == null)
            {
                return NotFound();
            }

            return View(customerSubModel);
        }

        // POST: CustomerSub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.CustomerSub == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CustomerSub'  is null.");
            }
            var customerSubModel = await _context.CustomerSub.FindAsync(id);
            if (customerSubModel != null)
            {
                _context.CustomerSub.Remove(customerSubModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerSubModelExists(int? id)
        {
          return (_context.CustomerSub?.Any(e => e.CustomerSubID == id)).GetValueOrDefault();
        }
    }
}
