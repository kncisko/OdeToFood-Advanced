using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OdeToFood.Data
{
    public class Filters : IFilters
    {
        public Expression<Func<Restaurant, bool>> ByName(string name = null)
        {
            return r => string.IsNullOrEmpty(name) || r.Name.ToLower().StartsWith(name.ToLower());
        }

        public Expression<Func<Restaurant, bool>> ByCuisine(CuisineType cuisine)
        {
            return r => cuisine == CuisineType.All || r.Cuisine == cuisine;
        }

        public Expression<Func<Restaurant, bool>> ByLocation(string city = null)
        {
            return r => city == null || r.Location.City == city;
        }
    }
}
