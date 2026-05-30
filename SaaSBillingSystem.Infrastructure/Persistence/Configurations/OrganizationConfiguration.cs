using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSBillingSystem.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SaaSBillingSystem.Infrastructure.Persistence.Configurations
{
    public class OrganizationConfiguration: IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(o => o.Name)
                .IsUnique();
        }
    }
}
