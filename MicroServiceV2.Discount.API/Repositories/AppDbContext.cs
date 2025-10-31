using System.Reflection;

namespace MicroServiceV2.Discount.API.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<Features.Discounts.Discount> Discounts { get; set; } = null!;

        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);

            return new AppDbContext(optionsBuilder.Options);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // MicroServiceV2.Catalog.API.Repositories içindeki tüm IEntityTypeConfiguration interfaceleri bulur ve uygular
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
