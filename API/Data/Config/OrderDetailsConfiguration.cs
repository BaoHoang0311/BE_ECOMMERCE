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
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.OrderNo).IsRequired().HasMaxLength(100);
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Product).WithMany()
                    .HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
 