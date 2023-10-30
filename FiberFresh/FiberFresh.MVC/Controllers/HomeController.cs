// Ignore Spelling: MVC

using FiberFresh.MVC.Models;
using FiberFresh.MVC.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace FiberFresh.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ClientMessage(ClientMessageModel clientMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mailMessage = new MailMessage();
            var smtpClient = new SmtpClient();

            var host = Environment.GetEnvironmentVariable("MAIL_HOST")!;
            var port = Environment.GetEnvironmentVariable("MAIL_PORT");
            var address = Environment.GetEnvironmentVariable("MAIL_ADDRESS")!;
            var password = Environment.GetEnvironmentVariable("MAIL_PASSWORD");

            mailMessage.From = new MailAddress(address);
            mailMessage.To.Add(address);
            mailMessage.Subject = $"Question from {clientMessage.Name}";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "<p>Name: " + clientMessage.Name + "</p><p>Email: " + clientMessage.Email + "</p><p>Message: " + clientMessage.Message + "</p>";

            smtpClient.Port = Convert.ToInt32(port);
            smtpClient.Host = host;
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(address, password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(mailMessage);

            return Ok();
        }
    }
} 