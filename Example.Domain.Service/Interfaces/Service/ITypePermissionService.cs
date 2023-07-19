using Example.Domain.Entities;

namespace Example.Domain.Service
{
    public interface ITypePermissionService
    {
        Task<IEnumerable<TypePermit>> GetAllAsync();
    }
}
