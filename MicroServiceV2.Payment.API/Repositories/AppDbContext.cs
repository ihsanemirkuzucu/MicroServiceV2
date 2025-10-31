namespace MicroServiceV2.Payment.API.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedNever();
                entity.Property(p => p.UserId).IsRequired();
                entity.Property(p => p.OrderCode).HasMaxLength(10).IsRequired();
                entity.Property(p => p.CreatedDate).IsRequired();
                entity.Property(p => p.Amount).HasPrecision(18,2).IsRequired();
                entity.Property(p => p.Status).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Payment> Payments { get; set; }
    }
}
