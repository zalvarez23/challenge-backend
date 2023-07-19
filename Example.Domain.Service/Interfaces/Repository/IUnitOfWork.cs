using Example.Domain.Entities;

namespace Example.Domain.Service
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Permission> PermissionRepository { get; }
        IRepository<TypePermit> TypePermitRepository { get; }
        Task SaveChangesAsync();
    }
}
