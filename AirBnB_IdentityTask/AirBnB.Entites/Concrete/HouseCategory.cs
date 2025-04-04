using System.ComponentModel.DataAnnotations;

namespace AirBnB.Entites.Concrete
{
    public class HouseCategory
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<House> Houses { get; set; } = new List<House>();
    }
}
