using LinqKit;
using OdeToFood.Core;
using OdeToFood.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OdeToFood.Data
{
    public class InMemoryRestauratData
    {
        IList<Restaurant> restaurants;

        public InMemoryRestauratData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant{ Id = Guid.NewGuid(), Cuisine = CuisineType.Indian, Name = "Royal India", Location = new Location("Zagreb", "Croatia") },
                new Restaurant{ Id = Guid.NewGuid(), Cuisine = CuisineType.Mexican, Name = "Mex Cantina", Location = new Location("Zagreb", "Croatia") },
                new Restaurant{ Id = Guid.NewGuid(), Cuisine = CuisineType.Italian, Name = "Franko's Pizza & Bar", Location = new Location("Zagreb", "Croatia") },
                new Restaurant{ Id = Guid.NewGuid(), Cuisine = CuisineType.Mediterranean, Name = "Pellegrino", Location = new Location("Šibenik", "Croatia") },
                new Restaurant{ Id = Guid.NewGuid(), Cuisine = CuisineType.Japanese, Name = "Takenoko", Location = new Location("Zagreb", "Croatia") },
                new Restaurant{ Id = Guid.NewGuid(), Cuisine = CuisineType.Mediterranean, Name = "Plavi podrum", Location = new Location("Opatija", "Croatia") }
            };
        }

        public Restaurant GetById(Guid id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> Get(params Expression<Func<Restaurant, bool>>[] filters)
        {
            var predicate = PredicateBuilder.New<Restaurant>();
            foreach (var filter in filters)
            {
                predicate = predicate.And(filter);
            }
            return restaurants.Where(predicate);
        }

        public IEnumerable<Location> GetLocationsForDropdown()
        {
            IList<Location> locations = new List<Location>();
            foreach (var r in restaurants)
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

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = Guid.NewGuid();
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public Restaurant Delete(Guid id)
        {
            Restaurant restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }
    }
}
