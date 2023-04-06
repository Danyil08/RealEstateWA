using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWA
{

    public partial class City
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "City name")]
        public string Title { get; set; } = null!;

        public virtual ICollection<Location> Locations { get; } = new List<Location>();
    }
}