using Example.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.Repository
{
    public class TypePermitMap : IEntityTypeConfiguration<TypePermit>
    {
        public void Configure(EntityTypeBuilder<TypePermit> builder)
        {
            builder.ToTable("TypePermits");
            builder.HasKey(c=>c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c=>c.Description).HasMaxLength(250).IsRequired();
        }
    }
}
