using Example.Domain.Entities;

namespace Example.Domain.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Permission entity)
        {
            await _unitOfWork.PermissionRepository.CreateAsync(entity);
        }

        public async Task CreateElasticAsync(Permission permission)
        {
            await _unitOfWork.PermissionRepository.CreateElasticAsync(permission);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _unitOfWork.PermissionRepository.GetAllAsync();
        }

        public async Task<IEnumerable<PermissionDto>> GetAllElasticAsync()
        {
            return await _unitOfWork.PermissionRepository.GetAllElasticAsync();
        }

        public async Task<Permission> GetByIdAsync(int id)
        {
            return await _unitOfWork.PermissionRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Permission entity)
        {
            await _unitOfWork.PermissionRepository.UpdateAsync(entity);
        }

        public async Task UpdateElasticAsync(Permission permission)
        {
            await _unitOfWork.PermissionRepository.UpdateElasticAsync(permission);
        }

        public void SendMessageTopic(OperationDto operation)
        {
            _unitOfWork.PermissionRepository.SendMessageTopic(operation);
        }
    }
}