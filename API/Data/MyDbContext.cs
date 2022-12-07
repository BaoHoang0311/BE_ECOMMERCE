using API.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata;

namespace API.Data
{
    public class MyDbContext :DbContext
    {
        public MyDbContext(DbContextOptions options) :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //// setting for Configuration Enitity, apply for ProductConfiguration

            modelBuilder.Entity<OrderDetail>()
                        .HasOne(e => e.Order)
                        .WithMany(e => e.OrderDetails)
                        .HasForeignKey(ur => ur.OrderId)
                        .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<OrderDetail>()
            //            .HasOne(e => e.Product)
            //            .WithMany(e => e.orderDetails)
            //            .HasForeignKey(ur => ur.ProductId)
            //            .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Order>()
            //            .HasOne(e => e.customer)
            //            .WithMany(e => e.order)
            //            .HasForeignKey(ur => ur.CustomerId)
            //            .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Order>()
            //            .HasOne(e => e.customer)
            //            .WithMany(e => e.order)
            //            .HasForeignKey(ur => ur.CustomerId)
            //            .OnDelete(DeleteBehavior.Cascade);

        }

        // Dbset
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
 