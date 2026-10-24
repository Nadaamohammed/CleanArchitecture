
using Microsoft.EntityFrameworkCore;

namespace Product.Infrastructure.persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Domain.Entity.Product> Products => Set<Domain.Entity.Product>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Entity.Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Price)
                .HasPrecision(18, 2);
        });
    }
}
