using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Abstract
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> Get(params Expression<Func<Restaurant, bool>>[] filter);
        Restaurant GetById(Guid id);
        IEnumerable<Location> GetLocationsForDropdown();
        Task<Restaurant> Add(Restaurant newRestaurant);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Delete(Guid id);
        int GetRestaurantsCount();
        Task<int> Commit();
    }
}
