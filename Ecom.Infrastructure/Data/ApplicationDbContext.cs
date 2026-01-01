using Ecom.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Entity<Order>()
           .HasMany(o => o.Items)
           .WithOne(i => i.Order)
           .HasForeignKey(i => i.OrderId);

            builder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderNo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(250);
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            });
        }

    }
}
