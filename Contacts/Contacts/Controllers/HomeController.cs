using Contacts.Data;
using Contacts.Models;
using Contacts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Contacts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactDataContext context;

        public HomeController(ContactDataContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var contacts = this.context.Contacts.Select(c => new ContactViewModel
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                DateOfBirth = c.DateOfBirth,
                PhoneNumber = c.PhoneNumber,
                Password = c.Password
            });
            return View(contacts);
        }

        public IActionResult Privacy()
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