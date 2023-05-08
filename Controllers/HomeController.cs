﻿using CapstoneWine.Models;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace CapstoneWine.Controllers
{
	public class HomeController : Controller
	{
        public IActionResult SendgridEmail()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        private readonly ILogger<HomeController> _logger;
        [HttpPost]
        public async Task<IActionResult> SendgridEmailSubmit(Emailmodel emailmodel)
        {
            ViewData["Message"] = "Email Sent!!!...";
            Example emailexample = new Example();
            await emailexample.Execute(emailmodel.From, emailmodel.To, emailmodel.Subject, emailmodel.Body, emailmodel.Body);
            return View("SendgridEmail");
        }

        public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
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
            public async Task Execute(string From, string To, string subject, string plainTextContent, string htmlContent)
            {
                var value = Environment.GetEnvironmentVariable("capstoneAPI");
                var client = new SendGridClient(value);
                var from = new EmailAddress(From);
                var to = new EmailAddress(To);
                htmlContent = "<strong>" + htmlContent + "</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
        }
    }
}