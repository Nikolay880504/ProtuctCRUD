using FIrstProductCRUD.Models;
using FIrstProtuctCRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace FIrstProductCRUD.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartProduct> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderElement> OrderElements { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartProduct>().
                HasOne(p => p.Product).
                WithMany(p => p.CartProducts).
                HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<OrderElement>().
                HasOne(o => o.Order).
                WithMany(o => o.Elements).
                HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<CartProduct>().
            Property(e => e.ID).UseIdentityColumn();

            modelBuilder.Entity<CartProduct>()
                .HasKey(c => c.ID);

            modelBuilder.Entity<OrderElement>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Order>()
                .HasKey(c => c.OrderId);
            modelBuilder.Entity<Order>().
         Property(o => o.OrderId).UseIdentityColumn();

            modelBuilder.Entity<OrderElement>().
            Property(e => e.Id).UseIdentityColumn();           
        }
    }
}
