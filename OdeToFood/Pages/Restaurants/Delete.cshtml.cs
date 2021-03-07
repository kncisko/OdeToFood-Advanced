using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data.Abstract;

namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;

        public DeleteModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public Restaurant Restaurant { get; set; }

        public IActionResult OnGet(Guid restaurantId)
        {
            Restaurant = _restaurantData.GetById(restaurantId);
            if (Restaurant == null) return RedirectToPage("./NotFound");
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid restaurantId)
        {
            Restaurant restaurant = _restaurantData.Delete(restaurantId);
            await _restaurantData.Commit();

            if (restaurant == null) return RedirectToPage("./NotFound");

            TempData["Message"] = $"{restaurant.Name} deleted.";
            return RedirectToPage("./List");
        }
    }
}
