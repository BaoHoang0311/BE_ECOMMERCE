using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Entites;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.OrderNo).IsRequired().HasMaxLength(100);
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18,2)");
            builder.HasOne(p=>p.customer).WithMany()
                .HasForeignKey(p=> p.CustomerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
