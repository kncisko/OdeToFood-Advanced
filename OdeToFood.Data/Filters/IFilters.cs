using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OdeToFood.Data
{
    public interface IFilters
    {
        Expression<Func<Restaurant, bool>> ByName(string name);
        Expression<Func<Restaurant, bool>> ByCuisine(CuisineType cuisine);
        Expression<Func<Restaurant, bool>> ByLocation(string city);
    }
}
