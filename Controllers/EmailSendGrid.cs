﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;

namespace CapstoneWine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSendGrid : ControllerBase
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = "SG.YTpEGcJzS8i06r - qREik8A.DVSXOfK6dEd6RDmkSg6jSTuG8Rw - 3zTF61tU6ErGSbQ";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("Xaviar.Rehu@techtorium.ac.nz", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("Xaviar.Rehu@techtorium.ac.nz", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync());
           
        }
    }
}
