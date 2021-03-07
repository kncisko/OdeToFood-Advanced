using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;
using OdeToFood.Data.Abstract;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
            CuisineTypes = _htmlHelper.GetEnumSelectList<CuisineType>().OrderBy(c => c.Text);
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> CuisineTypes { get; set; }

        public IActionResult OnGet(Guid? restaurantId)
        {
            if (restaurantId.HasValue)
            {
                Restaurant = _restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Restaurant.Id != Guid.Empty)
            {
                TempData["Message"] = "Restaurant updated.";
                Restaurant = _restaurantData.Update(Restaurant);
            }
            else 
            {
                TempData["Message"] = "Restaurant created.";
                await _restaurantData.Add(Restaurant);
            }
            await _restaurantData.Commit();
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id  });
        }
    }
}
