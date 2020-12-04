using Microsoft.EntityFrameworkCore;

namespace HungryDogs.Logic.DataContext
{
    class ProjectDbContext : DbContext
    {
        static string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=HungryDogsDb;Integrated Security=True";
        public DbSet<Entities.Persistence.Restaurant> RestaurauntSet { get; set; }
        public DbSet<Entities.Persistence.OpeningHour> OpeningHourSet { get; set; }
        public DbSet<Entities.Persistence.SpecialOpeningHour> SpecialOpeningHourSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var restaurantBuilder = modelBuilder.Entity<Entities.Persistence.Restaurant>();

            restaurantBuilder
                .ToTable(nameof(Entities.Persistence.Restaurant), "dbo")
                .HasKey(p => p.Id);
            restaurantBuilder
                .Property(p => p.RowVersion)
                .IsRowVersion();

            restaurantBuilder
                .Property(p => p.UniqueName)
                .HasMaxLength(128)
                .IsRequired();
            
            restaurantBuilder
                .HasIndex(p => p.UniqueName)
                .IsUnique();

            restaurantBuilder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);

            restaurantBuilder
                .Property(p => p.Email)
                .HasMaxLength(128)
                .IsRequired();

            restaurantBuilder
                .Property(p => p.OwnerName)
                .HasMaxLength(256)
                .IsRequired();

            var openingHourBuilder = modelBuilder.Entity<Entities.Persistence.OpeningHour>();

            openingHourBuilder
                .ToTable(nameof(Entities.Persistence.OpeningHour), "dbo")
                .HasKey(p => p.Id);

            openingHourBuilder
                .Property(p => p.RowVersion)
                .IsRowVersion();

            var specialOpeningHourBuilder = modelBuilder.Entity<Entities.Persistence.SpecialOpeningHour>();

            specialOpeningHourBuilder
                .ToTable(nameof(Entities.Persistence.SpecialOpeningHour), "dbo")
                .HasKey(p => p.Id);

            specialOpeningHourBuilder
                .Property(p => p.RowVersion)
                .IsRowVersion();

            base.OnModelCreating(modelBuilder);
        }
    }
}
