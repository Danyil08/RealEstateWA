using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWA
{

    public partial class Reservation
    {
        public int Id { get; set; }

        public int OfferId { get; set; }

        public int UserId { get; set; }

        public decimal Price { get; set; }

        public DateTime StDate { get; set; }

        public DateTime FinDate { get; set; }

        public virtual Offer Offer { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}