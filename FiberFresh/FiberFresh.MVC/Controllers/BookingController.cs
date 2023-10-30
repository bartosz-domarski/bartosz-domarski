// Ignore Spelling: MVC Dto

using FiberFresh.Application.Dtos;
using FiberFresh.Application.Services;
using FiberFresh.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FiberFresh.MVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IFiberFreshService _service;

        public BookingController(IFiberFreshService service)
        {
            _service = service;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> Create(BookingDto bookingDto)
        {
            if (bookingDto.FirstName == "Valuation")
            {
                if (bookingDto.Services != null)
                {
                    var totalPrice = new PriceRange(0, 0);

                    foreach (var serviceDto in bookingDto.Services)
                    {
                        if (serviceDto.CBM == 0)
                        {
                            var service = new Service(serviceDto.Furniture, serviceDto.Fabric, serviceDto.Size);
                            totalPrice.Add(service.PriceRange);
                        }
                        else
                        {
                            var service = new Service(serviceDto.Furniture, serviceDto.Fabric, serviceDto.CBM);
                            totalPrice.Add(service.PriceRange);
                        }
                    }

                    return Json(new { Valuation = totalPrice.ToString() });
                }

                return ValidationProblem();
            }
            else
            {
                var response = await _service.Create(bookingDto);

                if (response != System.Net.HttpStatusCode.OK)
                {
                    return ValidationProblem();
                }
                else
                {
                    return Accepted();
                }
            }
        }
    }
}
