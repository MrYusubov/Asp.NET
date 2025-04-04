﻿using System.ComponentModel.DataAnnotations;
using AirBnB.Core.Abstraction;
using AirBnB.Entites.Identity;

namespace AirBnB.Entites.Concrete
{
    public class House:IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public decimal PricePerNight { get; set; }

        [Required, MaxLength(255)]
        public string Location { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public HouseCategory Category { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<DynamicPricing> DynamicPricings { get; set; } = new List<DynamicPricing>();

        [Range(0, 5)]
        public double Rating { get; set; } = 0;
    }
}
