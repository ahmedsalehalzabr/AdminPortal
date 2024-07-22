using AdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminPortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) 
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulties
            //Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("66c1acdb-cafc-44e4-be04-b8d9162101e2"),
                    Name = "Easy"
                },
                 new Difficulty()
                {
                    Id = Guid.Parse("2e0eaf1b-b7ef-42a7-a914-9c1da89ebff3"),
                    Name = "Medium"
                },
                  new Difficulty()
                {
                    Id = Guid.Parse("26636693-5e7d-470d-ab08-0006b1abacfc"),
                    Name = "Hard"
                }
            };

            //seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            //seed data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("598403e6-88c6-40a6-a51d-1abcd1e23dc3"),
                    Name = "Ahmed Alzabr",
                    Code = "AA",
                    RegionImageUrl = "https://images.pexels.com/photos/26082992/pexels-photo-26082992/free-photo-of-alberoxalbero.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("b0d641f2-f07d-45fe-afc0-1a84eca538c3"),
                    Name = "Ahmed Saleh",
                    Code = "AS",
                    RegionImageUrl = "https://images.pexels.com/photos/26082992/pexels-photo-26082992/free-photo-of-alberoxalbero.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("2a1d2fad-3446-4796-8524-5fbbea4c15e0"),
                    Name = "Mohammed Zaid",
                    Code = "MZ",
                    RegionImageUrl = "https://images.pexels.com/photos/26082992/pexels-photo-26082992/free-photo-of-alberoxalbero.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("9c10ca47-6534-4557-b641-67d45ac5758b"),
                    Name = "Samerh Ali",
                    Code = "SA",
                    RegionImageUrl = "https://images.pexels.com/photos/26082992/pexels-photo-26082992/free-photo-of-alberoxalbero.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
