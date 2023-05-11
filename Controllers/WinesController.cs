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
    public class WinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wines
        public async Task<IActionResult> Index()
        {
            // Return an error message if the Wine entity set is null
            if (_context.Wines == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Wines' is null.");
            }
            // Otherwise, return the Wines entity set as a view
            return View(await _context.Wines.ToListAsync());
        }

        // GET: Wines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the Wine object with the specified id
            var wine = await _context.Wines.FirstOrDefaultAsync(m => m.WineID == id);

            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        // GET: Wines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WineID,WineName,Image,Price,Blurb,Quantity,Type,Category")] WinesModel wine)
        {
            if (ModelState.IsValid)
            {
                int newWineID = 1; // default value for the new WineID

                // Check whether there are any records in the Wines table
                if (_context.Wines.Any())
                {
                    // Generate a new WineID
                    newWineID = _context.Wines.Max(s => s.WineID) + 1;
                }

                // Set the new WineID value
                wine.WineID = newWineID;

                // Add the wine object to the context
                _context.Add(wine);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Redirect to the Index page
                return RedirectToAction(nameof(Index));
            }

            // If the ModelState is not valid, return the current view with the wine object
            return View(wine);
        }

        // GET: Wines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the Wine object with the specified id
            var wine = await _context.Wines.FindAsync(id);

            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }


        // POST: Wines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WineID,WineName,Image,Price,Blurb,Quantity,Type,Category")] WinesModel wine)
        {
            if (id != wine.WineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WineExists(wine.WineID))
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
            return View(wine);
        }

        // GET: Wines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Wines == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines
                .FirstOrDefaultAsync(m => m.WineID == id);
            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        // POST: Wines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wines == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Wine'  is null.");
            }
            var wine = await _context.Wines.FindAsync(id);
            if (wine != null)
            {
                _context.Wines.Remove(wine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineExists(int id)
        {
          return (_context.Wines?.Any(e => e.WineID == id)).GetValueOrDefault();
        }
    }
}
