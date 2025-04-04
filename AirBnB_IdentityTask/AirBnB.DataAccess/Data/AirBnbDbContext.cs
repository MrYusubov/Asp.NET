using AirBnB.Entites.Concrete;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.DataAccess.Data
{
    public class AirBnbDbContext : DbContext
    {
        public AirBnbDbContext(DbContextOptions<AirBnbDbContext> options)
            : base(options)
        {
        }
        public AirBnbDbContext() { }

        public DbSet<House> Houses { get; set; }
        public DbSet<HouseCategory> HouseCategories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<DynamicPricing> DynamicPricings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AirBnB;Integrated Security=True;");
            }
        }
    }
}
