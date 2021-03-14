using System;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data.Abstract;

namespace OdeToFood.ViewComponents
{
    public class RestaurantCountViewComponent : ViewComponent
    {
        private readonly IRestaurantData _restaurantData;

        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IViewComponentResult Invoke()
        {
            var count = _restaurantData.GetRestaurantsCount();
            return View(count);
        }
    }
}
