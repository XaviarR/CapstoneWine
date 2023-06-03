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
	public class WinesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private IWebHostEnvironment Environment;


		public WinesController(ApplicationDbContext context, IWebHostEnvironment _environment)
		{
			_context = context;
			Environment = _environment;

		}

		// GET: Wines
		public async Task<IActionResult> Index(string searchString)
		{

			ViewData["CurrentFilter"] = searchString;

			var wines = from wine in _context.Wines
						select wine;
			if (!String.IsNullOrEmpty(searchString))
			{
				wines = wines.Where(w => w.WineName.Contains(searchString));
				return View(wines);
			}

			if (String.IsNullOrEmpty(searchString))
			{
				ViewData["CurrentFilter"] = "";
			}

			var wineList = _context.Wines.ToList();
			return View(wineList);
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
			ViewData["ErrorMessage"] = "";
			return View();
		}

		// POST: Wines/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("WineID,WineName,Image,Price,Blurb,Quantity,Type,Category")] WinesModel wine, List<IFormFile> postedFiles)
		{
			string path = Path.Combine(this.Environment.WebRootPath, "Images", "uploads");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}


			if (postedFiles == null || postedFiles.Count == 0)
			{
				ViewData["ErrorMessage"] = "You must add an image";
				return RedirectToAction("Create");
			}


			foreach (IFormFile postedFile in postedFiles)
			{
				if (postedFile != null)
				{
					string fileName = Path.GetFileName(postedFile.FileName);
					string filePath = Path.Combine(path, fileName);

					using (FileStream stream = new FileStream(filePath, FileMode.Create))
					{
						await postedFile.CopyToAsync(stream);
					}
					wine.Image = "/Images/uploads/" + fileName;
				}
			}


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

			ViewData["ImagePath"] = wine.Image;

			return View(wine);
		}


		// POST: Wines/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("WineID,WineName,Image,Price,Blurb,Quantity,Type,Category")] WinesModel wine, List<IFormFile> postedFiles)
		{
			string path = Path.Combine(this.Environment.WebRootPath, "Images", "uploads");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			foreach (IFormFile postedFile in postedFiles)
			{
				if (postedFile != null)
				{
					string fileName = Path.GetFileName(postedFile.FileName);
					string filePath = Path.Combine(path, fileName);

					using (FileStream stream = new FileStream(filePath, FileMode.Create))
					{
						await postedFile.CopyToAsync(stream);
					}
					wine.Image = "/Images/uploads/" + fileName;
				}
			}



			if (id != wine.WineID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(wine);
					if (postedFiles == null || postedFiles.Count == 0)
					{
						_context.Entry(wine).Property(x => x.Image).IsModified = false;
					}
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
				return RedirectToAction("Index");
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
				// Delete the image file if it exists
				if (!string.IsNullOrEmpty(wine.Image))
				{
					string filePath = Path.Combine(this.Environment.WebRootPath, wine.Image.TrimStart('/'));
					if (System.IO.File.Exists(filePath))
					{
						System.IO.File.Delete(filePath);
					}
				}

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
