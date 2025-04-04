using AirBnB.Entites.Identity;
using System.ComponentModel.DataAnnotations;

namespace AirBnB.Entites.Concrete
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public int HouseId { get; set; }
        public virtual User User { get; set; }
        public virtual House House { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
