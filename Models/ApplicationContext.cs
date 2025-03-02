using Microsoft.EntityFrameworkCore;
using UITraining.Models.DB;

namespace UITraining.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.supplier)
                .WithMany(s => s.products)
                .HasForeignKey(p => p.IdSupplier);

            base.OnModelCreating(modelBuilder);
        }
    }
}
