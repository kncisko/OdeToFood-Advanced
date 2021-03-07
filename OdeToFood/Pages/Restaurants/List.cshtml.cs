using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;
using OdeToFood.Data.Abstract;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IRestaurantData _restaurantData;
        private readonly IFilters _filters;

        public IEnumerable<Restaurant> Restaurants { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public CuisineType SearchCuisine { get; set; } = CuisineType.All;

        [BindProperty(SupportsGet = true)]
        public IEnumerable<Location> Locations { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string SearchLocation { get; set; }

        public ListModel(IConfiguration config, IRestaurantData restaurantData, IFilters filters)
        {
            _config = config;
            _restaurantData = restaurantData;
            _filters = filters;
            Locations = _restaurantData.GetLocationsForDropdown();
        }
        public void OnGet()
        {
            Restaurants = _restaurantData.Get(_filters.ByName(SearchTerm), _filters.ByCuisine(SearchCuisine), _filters.ByLocation(SearchLocation));
        }
    }
}