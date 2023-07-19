using Example.Domain.Entities;
using Example.Domain.Service;
using Nest;

namespace Example.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RegistrationPermissionContext _context;
        private readonly Domain.Service.IRepository<Permission> _permitRepository;
        private readonly Domain.Service.IRepository<TypePermit> _typePermissionRepository;
        private readonly IElasticClient _elasticClient;

        public UnitOfWork(RegistrationPermissionContext context, IElasticClient elasticClient)
        {
            _context = context;
            _elasticClient = elasticClient;
        }

        public Domain.Service.IRepository<Permission> PermissionRepository =>
            _permitRepository ?? new BaseRepository<Permission>(_context, _elasticClient);

        public Domain.Service.IRepository<TypePermit> TypePermitRepository =>
            _typePermissionRepository ?? new BaseRepository<TypePermit>(_context, _elasticClient);

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}