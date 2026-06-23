using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.Provider)
                   .HasMaxLength(100);

            builder.Property(x => x.ExternalPaymentId)
                   .HasMaxLength(200);

            builder.Property(x => x.FailureReason)
                   .HasMaxLength(500);

            builder.Property(p => p.Status)
                .IsRequired().
                HasConversion<int>();

            builder.Property(p => p.Currency)
                .IsRequired().
                HasConversion<int>();
        }
    }
}
