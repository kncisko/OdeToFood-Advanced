using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OdeToFood.Core
{
    public class Restaurant
    {
        public Guid Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }

        public Location Location { get; set; }
    }
}
