// Ignore Spelling: MVC

using FiberFresh.Application.Services;
using FiberFresh.Domain.Entities;
using FiberFresh.MVC.Models.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FiberFresh.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IFiberFreshService _service;

        public DashboardController(IFiberFreshService service, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _service = service;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var adminLogin = Environment.GetEnvironmentVariable("ADMIN_LOGIN")!;
            var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD")!;

            if (!_userManager.Users.Any(x => x.UserName == adminLogin))
            {
                var adminIdentity = new IdentityUser(adminLogin);
                var resultCreate = await _userManager.CreateAsync(adminIdentity, adminPassword);
                
            }

            if (loginModel.Login == adminLogin && loginModel.Password == adminPassword)
            {
                var result = await _signInManager.PasswordSignInAsync(adminLogin, adminPassword, true, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
            else
            {
                return View();
            }
        }

        public IActionResult Index()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDates()
        {
            var dateOfServicePool = await _service.GetDates();
            var json = JsonConvert.SerializeObject(dateOfServicePool);

            return Json(json);
        }

        [HttpPost]
        public async Task<IActionResult> AddDate(DateOfService dateOfService)
        {
            await _service.AddDate(dateOfService);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddDates(List<DateOfService> dateOfServicePool)
        {
            await _service.AddDates(dateOfServicePool);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDate(DateOfService dateOfService)
        {
            await _service.DeleteDate(dateOfService);

            return Ok();
        }
    }
}
