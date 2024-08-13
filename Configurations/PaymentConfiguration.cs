using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Configurations
{
    public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.OrderId).IsRequired();
            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(5,2)");
            builder.HasCheckConstraint("CK_Amount", "Amount>0");
        }
    }
}
