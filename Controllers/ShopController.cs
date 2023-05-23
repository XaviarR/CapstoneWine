using CapstoneWine.Data;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneWine.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var wines = from wine in _context.Wines
                        select wine;
            if (!String.IsNullOrEmpty(searchString))
            {
                wines = wines.Where(w => w.WineName.Contains(searchString));
                return View(wines);
            }

            var wineList = _context.Wines.ToList();
            return View(wineList);
        }
    }
}
