using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Core
{
    public enum CuisineType
    {
        [Display(Name = "All Cuisines")] All = 0,
        [Display(Name = "Mexican")] Mexican = 1,
        [Display(Name = "Italian")] Italian = 2,
        [Display(Name = "Indian")] Indian = 3,
        [Display(Name = "Japanese")] Japanese = 4,
        [Display(Name = "Mediterranean")] Mediterranean = 5,
        [Display(Name = "Pizza")] Pizza = 6
    }
}
