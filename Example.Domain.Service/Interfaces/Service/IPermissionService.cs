using Example.Domain.Entities;

namespace Example.Domain.Service
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<IEnumerable<PermissionDto>> GetAllElasticAsync();
        Task<Permission> GetByIdAsync(int id);
        Task CreateAsync(Permission entity);
        Task CreateElasticAsync(Permission permission);
        Task UpdateAsync(Permission entity);
        Task UpdateElasticAsync(Permission permission);
        void SendMessageTopic(OperationDto operationDto);
    }
}
