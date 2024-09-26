using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Model
{
    public class VillaDbContext : DbContext
    {

        public VillaDbContext(DbContextOptions<VillaDbContext> options) : base(options) { }



        public DbSet<Villa> Villas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<Villa>().HasKey(x => x.Id);

            modelBuilder.Entity<Villa>().HasData(
                 new Villa()
                 {
                     Id = 1,
                     Name = "Villa 1",
                     Details = "This is a beautiful villa with 1 bedroom and 1 bathroom.",
                     Rate = 101,
                     Sqft = 2001,
                     Occupancy = 5,
                     ImageUrl = "villa1.jpg",
                     Amenity = "Swimming pool, jacuzzi, gym",
                     CreatedAt = DateTime.Now,
                     UpdatedAt = DateTime.Now
                 },
                 new Villa

                 {
                     Id = 2,
                     Name = "Villa 2",
                     Details = "This is a beautiful villa with 2 bedrooms and 2 bathrooms.",
                     Rate = 102,
                     Sqft = 2002,
                     Occupancy = 6,
                     ImageUrl = "villa2.jpg",
                     Amenity = "Swimming pool, jacuzzi, gym",
                     CreatedAt = DateTime.Now,
                     UpdatedAt = DateTime.Now
                 },
                    new Villa
                    {
                        Id = 3,

                        Name = "Villa 3",
                        Details = "This is a beautiful villa with 3 bedrooms and 3 bathrooms.",
                        Rate = 103,
                        Sqft = 2003,
                        Occupancy = 7,
                        ImageUrl = "villa3.jpg",
                        Amenity = "Swimming pool, jacuzzi, gym",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },

                    new Villa
                    {
                        Id = 4,

                        Name = "Villa 4",
                        Details = "This is a beautiful villa with 4 bedrooms and 4 bathrooms.",
                        Rate = 104,
                        Sqft = 2004,
                        Occupancy = 8,
                        ImageUrl = "villa4.jpg",
                        Amenity = "Swimming pool, jacuzzi, gym",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },

                     new Villa
                     {
                         Id = 5,

                         Name = "Villa 5",
                         Details = "This is a beautiful villa with 5 bedrooms and 5 bathrooms.",
                         Rate = 105,
                         Sqft = 2005,
                         Occupancy = 9,
                         ImageUrl = "villa5.jpg",
                         Amenity = "Swimming pool, jacuzzi, gym",
                         CreatedAt = DateTime.Now,
                         UpdatedAt = DateTime.Now
                     });




        }

    }
}
