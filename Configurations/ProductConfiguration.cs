using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(6,2)");
            builder.HasCheckConstraint("CK_PriceCheck", "Price>0");
            builder.HasCheckConstraint("CK_StockCheck", "Stock>=0");
        }
    }
}
