using CapstoneWine.Data;
using CapstoneWine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics;

namespace CapstoneWine.Controllers
{
    public class HomeController : Controller
    {
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
        {
            if(!Request.Cookies.ContainsKey("AgeVerified"))
            {
                return RedirectToAction("VerifyAge");
            }
            return View();
        }
        
        public IActionResult VerifyAge() {
            return View();
        }
		/*AgeCheck*/
		[HttpPost]
		public IActionResult VerifyAge(DateTime dateOfBirth)
		{
			DateTime currentDate = DateTime.Now;
			int age = currentDate.Year - dateOfBirth.Year;

			//check if user is above min age
			int minimumAge = 18;
			if (age >= minimumAge)
			{
				HttpContext.Response.Cookies.Append("AgeVerified", "true");
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return Redirect("https://www.alcohol.org.nz");
			}
		}


		[Authorize]
        public IActionResult Subscription()
		{
			return View();
		}

		public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult Help()
		{
			return View();
		}

        public IActionResult TermsAndConditions()
        {
            return View();
        }

        public IActionResult LiquorLicense()
        {
            return View();
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}