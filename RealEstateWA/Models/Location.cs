using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWA
{

    public partial class Location
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        [Required(ErrorMessage = "Area name must be provided")]
        [Display(Name = "Area name")]
        public string Area { get; set; } = null!;
        [Required(ErrorMessage = "Street name must be provided")]
        [Display(Name = "Street name")]
        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "Overview must be provided")]
        [Display(Name = "Briefly about")]
        public string BrieflyAbout { get; set; } = null!;
        public virtual City City { get; set; } = null!;
        public virtual ICollection<Offer> Offers { get; } = new List<Offer>();
    }
}