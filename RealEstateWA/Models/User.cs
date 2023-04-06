using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWA
{

    public partial class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "This field must contain at least 3 letters")]
        [MaxLength(63, ErrorMessage = "This field must contain less than 64 letters")]
        public string FullName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(9, ErrorMessage = "This field must contain exactly 9 digits", MinimumLength = 9)]
        public string PhNumber { get; set; } = null!;
        [Required(ErrorMessage = "This field is required")]
        public bool Sex { get; set; }
        [Required(ErrorMessage = "Write at least something about yourself")]
        [StringLength(255, ErrorMessage = "This field must contain less than 256 digits")]
        public string Overview { get; set; } = null!;
        [Required(ErrorMessage = "This field is required")]
        public bool Premium { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime RegistrationDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public bool UserType { get; set; }

        public virtual ICollection<Offer> Offers { get; } = new List<Offer>();

        public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
    }
}