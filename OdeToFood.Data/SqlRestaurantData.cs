using LinqKit;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using OdeToFood.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Restaurant> Get(params Expression<Func<Restaurant, bool>>[] filters)
        {
            var predicate = PredicateBuilder.New<Restaurant>();
            foreach (var filter in filters)
            {
                predicate = predicate.And(filter);
            }
            return _dbContext.Restaurants.Where(predicate).Include(r => r.Location);
        }

        public Restaurant GetById(Guid id)
        {
            return _dbContext.Restaurants.Include(r => r.Location).FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Location> GetLocationsForDropdown()
        {
            IList<Location> locations = new List<Location>();
            foreach (var r in _dbContext.Restaurants.Include(r => r.Location))
            {
                if (r.Location != null)
                {
                    if (locations.Any(l => l.City == r.Location.City) && locations.Any(l => l.Country == r.Location.Country))
                    {
                        continue;
                    }
                    locations.Add(r.Location);
                }
            }

            return locations.OrderBy(l => l.Country).ThenBy(l => l.City);
        }

        public async Task<Restaurant> Add(Restaurant newRestaurant)
        {
            await _dbContext.Restaurants.AddAsync(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = _dbContext.Restaurants.Attach(updatedRestaurant);
            restaurant.State = EntityState.Modified;

            return updatedRestaurant;
        }

        public Restaurant Delete(Guid id)
        {
            Restaurant restaurant = GetById(id);
            if (restaurant != null)
            {
                _dbContext.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public int GetRestaurantsCount()
        {
            return _dbContext.Restaurants.Count();
        }
    }
}
