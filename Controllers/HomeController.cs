using CapstoneWine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics;

namespace CapstoneWine.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult SendgridEmail()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
       
        
        /*Email*/
		private readonly ILogger<HomeController> _logger;
        [HttpPost]
        public async Task<IActionResult> SendgridEmailSubmit(Emailmodel emailmodel)
        {
            ViewData["Message"] = "Email Sent!!!...";
            Example emailexample = new Example();
            await emailexample.Execute(emailmodel.To, emailmodel.To, emailmodel.To, emailmodel.To);//needed to parse 4 values from async task Execute
            return View("SendgridEmail");
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        public IActionResult Checkout()
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

        internal class Example
        {
            public async Task Execute(string To, string subject, string plainTextContent, string htmlContent)
            {
                var apiKey = APIFILE.apiKey;//api key
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("xaviar.rehu@techtorium.ac.nz");//email that sends messages to user
                var to = new EmailAddress(To);//user inputs own email
                subject = "Thank you for registering with us";//message that is emailed to user
                plainTextContent = "Welcome to WiNeZ";
                htmlContent = "<strong>" + htmlContent + "</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
        }
    }
}