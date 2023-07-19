using Example.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Repository
{
    public class RegistrationPermissionContext : DbContext
    {
        public RegistrationPermissionContext(DbContextOptions<RegistrationPermissionContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PermissionMap());
            modelBuilder.ApplyConfiguration(new TypePermitMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Permission>? Permits { get; set; }
        public DbSet<TypePermit>? TypePermits { get; set; }

    }
}
