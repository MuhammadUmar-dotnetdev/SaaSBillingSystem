using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Infrastructure.Persistence.Configurations
{
    public class InvoiceConfiguration: IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Amount)
                .HasPrecision(18, 2);

            builder.Property(i => i.Status)
                .HasConversion<int>();

            builder.Property(i => i.InvoiceNumber)
                   .HasMaxLength(200);

            builder.HasOne(i => i.Subscription)
                .WithOne(s => s.Invoice)
                .HasPrincipalKey<Subscription>(s => s.Id)
                .HasForeignKey<Invoice>(i => i.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
