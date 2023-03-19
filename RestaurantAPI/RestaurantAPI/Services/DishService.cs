using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        List<DishDto> GetAll(int id);
        DishDto GetById(int restaurantId, int dishId);
        void DeleteById(int restaurantId, int dishId);
        void Delete(int restaurantId);
    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _dbContext.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
                throw new NotfoundException("Restaurant not found");

            return restaurant;
        }

        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishEntity = _mapper.Map<Dish>(dto);
            dishEntity.RestaurantId = restaurantId;

            _dbContext.Dishes.Add(dishEntity);
            _dbContext.SaveChanges();

            return dishEntity.Id;
        }

        public void DeleteById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null)
                throw new NotfoundException("Dish not found");

            _dbContext.Dishes.Remove(dish);
            _dbContext.SaveChanges();
        }

        public void Delete(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            _dbContext.Dishes.RemoveRange(restaurant.Dishes);
            _dbContext.SaveChanges();
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = _dbContext.Dishes
                .Where(r => r.RestaurantId == restaurantId)
                .FirstOrDefault(d => d.Id == dishId);

            if (dish is null)
                throw new NotfoundException("Dish not found");

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }

        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishesDto = _mapper.Map<List<DishDto>>(restaurant.Dishes);
            return dishesDto;
        }
    }
}
