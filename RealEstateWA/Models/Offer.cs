using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWA
{

    public partial class Offer
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }

        public bool Available { get; set; }
        [Required(ErrorMessage = "This field is reqired")]
        [MaxLength(1023, ErrorMessage = "This field must contain less than 1023 letters")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "This field is reqired")]
        public DateTime PostDate { get; set; }
        [Required(ErrorMessage = "This field is reqired")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "This field is reqired")]
        public string EstateType { get; set; } = null!;

        public virtual Location Location { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

        public virtual User User { get; set; } = null!;
    }
}