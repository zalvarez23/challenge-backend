using Example.Domain.Entities;

namespace Example.Domain.Service
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<PermissionDto>> GetAllElasticAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task CreateElasticAsync(Permission permission);
        Task UpdateAsync(T entity);
        Task UpdateElasticAsync(Permission permission);
        void SendMessageTopic(OperationDto operationDto);
    }
}