using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("/api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService) 
        { 
            _dishService = dishService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DishDto>> GetAll([FromRoute]int restaurantId) 
        {
            var dishesDto = _dishService.GetAll(restaurantId);
            return Ok(dishesDto);
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute]int restaurantId, [FromRoute]int dishId)
        {
            var dishDto = _dishService.GetById(restaurantId, dishId);
            return Ok(dishDto);
        }

        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId, [FromBody]CreateDishDto dto)
        {
            var id = _dishService.Create(restaurantId, dto);
            return Created($"/api/restaurant/{restaurantId}/dish/{id}", null);
        }

        [HttpDelete("{dishId}")]
        public ActionResult DeleteById([FromRoute]int restaurantId, [FromRoute]int dishId)
        {
            _dishService.DeleteById(restaurantId , dishId);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _dishService.Delete(restaurantId);
            return NoContent();
        }
    }
}
