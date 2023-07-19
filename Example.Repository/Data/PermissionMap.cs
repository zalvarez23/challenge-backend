using Example.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.Repository
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permits");
            builder.HasKey(c => c.Id);
            builder.Property(c=>c.Id).ValueGeneratedOnAdd();

            builder.Property(c=>c.EmployeeName).HasMaxLength(250).IsRequired();
            builder.Property(c => c.LastNameEmployee).HasMaxLength(250).IsRequired();
            builder.Property(c=>c.TypePermit).IsRequired();
            builder.Property(c=>c.DatePermission).IsRequired();
        }
    }
}
