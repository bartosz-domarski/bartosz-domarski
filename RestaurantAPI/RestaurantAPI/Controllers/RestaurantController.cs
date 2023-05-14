using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery]RestaurantQuery query)
        {
            var restaurantsDto = _service.GetAll(query);
            return Ok(restaurantsDto);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurantDto = _service.GetById(id);
            return Ok(restaurantDto);
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var id = _service.Create(dto);
            return Created($"/api/restaurant/{id}", null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public ActionResult Update([FromRoute]int id, [FromBody]UpdateRestaurantDto dto)
        {
            _service.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public ActionResult Delete([FromRoute]int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}