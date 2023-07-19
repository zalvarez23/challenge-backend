using Example.Domain.Entities;
using Example.Domain.Service;
using Nest;

namespace Example.Repository
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(RegistrationPermissionContext context, IElasticClient elasticClient) : base(context,
            elasticClient)
        {
        }
    }
}