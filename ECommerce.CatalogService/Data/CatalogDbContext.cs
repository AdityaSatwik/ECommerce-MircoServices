using ECommerce.CatalogService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.CatalogService.Data
{
    /// <summary>
    /// Database context for the Catalog Service, managing product entities via EF Core.
    /// </summary>
    public class CatalogDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogDbContext"/> class.
        /// </summary>
        /// <param name="options">The db context options.</param>
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the products data set.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Product>().Property<DateTime>("LastModified");
        }

        /// <summary>
        /// Saves changes to the database and updates last modified timestamps.
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Product>())
            {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                {
                    entry.Property("LastModified").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
