using System;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Core
{
    public class Location
    {
        public Location() {}
        public Location(string city, string country)
        {
            Id = Guid.NewGuid();
            City = city;
            Country = country;
        }
        public Guid Id { get; set; }
        [Required, StringLength(100)]
        public string City { get; set; }
        [Required, StringLength(100)]
        public string Country { get; set; }
        public string Address { get; set; }
        public string PostalNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
