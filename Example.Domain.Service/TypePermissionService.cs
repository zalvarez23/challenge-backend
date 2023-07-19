using Example.Domain.Entities;

namespace Example.Domain.Service
{
    public class TypePermissionService : ITypePermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypePermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
      
        public async Task<IEnumerable<TypePermit>> GetAllAsync()
        {
            return await _unitOfWork.TypePermitRepository.GetAllAsync();
        }
    }
}