using ContactsWebApp.Helper;
using ContactsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ContactsWebAPI.Model;
using Newtonsoft.Json;

namespace ContactsWebApp.Controllers
{
    public class HomeController : Controller
    {
        ContactsAPI _api = new ContactsAPI();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var contactModel = new List<ContactModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("api/Contact");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                contactModel = JsonConvert.DeserializeObject<List<ContactModel>>(result);
            }
            return View(contactModel);
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